namespace Shared.Common.Kafka;

public interface IKafkaHandler<Tk, Tv>
{
    Task HandleAsync(Tk key, Tv value);
}