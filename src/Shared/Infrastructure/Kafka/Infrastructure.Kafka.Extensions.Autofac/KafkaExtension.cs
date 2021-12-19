using Autofac;
using Infrastructure.Kafka.Consumer;
using Shared.Common;
using Shared.Common.Kafka;

namespace Infrastructure.Kafka.Extensions.Autofac;

public static class KafkaExtension
{
    public static void AddKafkaConsumer<Tk, Tv, THandler, TContext>(this ContainerBuilder builder, string topic, string groupId, string bootstrapServers) where THandler : class, IKafkaHandler<Tk, Tv>
    {
        builder.RegisterType<THandler>().As<IKafkaHandler<Tk, Tv>>().InstancePerLifetimeScope();
        builder.RegisterType<KafkaBackgroundConsumer<Tk, Tv, TContext>>().As<IApplicationLifecycleHook>()
            .WithParameter(new TypedParameter(typeof(KafkaConsumerConfig), new KafkaConsumerConfig(topic, groupId, bootstrapServers)))
            .SingleInstance();
    }
}