using System.Text;
using Confluent.Kafka;
using Shared.Common.Extensions;

namespace Infrastructure.Kafka.Consumer;

internal sealed class KafkaDeserializer<T> : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        var jsonData = Encoding.UTF8.GetString(data);

        return jsonData.ToObject<string>().ToObject<T>();
    }
}