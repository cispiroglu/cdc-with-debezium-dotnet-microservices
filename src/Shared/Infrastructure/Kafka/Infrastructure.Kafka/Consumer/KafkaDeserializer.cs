using System.Text;
using Confluent.Kafka;
using Shared.Common.Extensions;

namespace Infrastructure.Kafka.Consumer;

internal sealed class KafkaDeserializer<T> : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        var dataJsonString = Encoding.UTF8.GetString(data);

        // deserializing twice because of double serialization of event payload.
        var normalizedJsonString = dataJsonString.ToObject<string>();

        return normalizedJsonString.ToObject<T>();
    }
}