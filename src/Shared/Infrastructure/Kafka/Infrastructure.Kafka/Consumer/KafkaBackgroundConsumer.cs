using Confluent.Kafka;
using Shared.Common;
using Shared.Common.Kafka;

namespace Infrastructure.Kafka.Consumer;

public class KafkaBackgroundConsumer<Tk, Tv, TContext> : IApplicationLifecycleHook
{
    private readonly KafkaConsumerConfig _consumerConfiguration;
    private readonly ILifetimeScopeProvider _lifetimeScopeProvider;
    
    public KafkaBackgroundConsumer(ILifetimeScopeProvider lifetimeScopeProvider, KafkaConsumerConfig consumerConfiguration)
    {
        _lifetimeScopeProvider = lifetimeScopeProvider;
        _consumerConfiguration = consumerConfiguration;
    }

    public void OnStarted()
    {
        Task.Factory.StartNew(() => ConsumeTopic(default), default, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        Console.WriteLine($"Kafka Consumer Started: {typeof(Tv).Name}");
    }

    public void OnStopRequested()
    {
        Console.WriteLine($"Kafka Consumer Stopping: {typeof(Tv).Name}");
    }

    public void OnStopped()
    {
        Console.WriteLine($"Kafka Consumer Stopped: {typeof(Tv).Name}");
    }
    
    private async Task ConsumeTopic(CancellationToken stoppingToken)
    {
        var handler = _lifetimeScopeProvider.Resolve<IKafkaHandler<Tk, Tv>>();
        var context = _lifetimeScopeProvider.Resolve<TContext>();
        
        var builder = new ConsumerBuilder<Tk, Tv>(_consumerConfiguration).SetValueDeserializer(new KafkaDeserializer<Tv>());
        
        using var consumer = builder.Build();
        consumer.Subscribe(_consumerConfiguration.Topic);

        while (stoppingToken.IsCancellationRequested == false)
        {
            try
            {
                var result = consumer.Consume(3000);

                if (result == null) 
                    continue;
                    
                Console.WriteLine($"Handling Consumer: {handler.GetType().Name}");
                await handler.HandleAsync(result.Message.Key, result.Message.Value);

                if (context is IDbContext dbContext)
                {
                    await dbContext.SaveEntitiesAsync(stoppingToken);
                    consumer.Commit(result);
                }
                else
                    Console.WriteLine($"IDbContext is null can't handle message! Key: {result.Message.Key}, Message: {result.Message.Value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}