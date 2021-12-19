using Confluent.Kafka;

namespace Infrastructure.Kafka.Consumer;

public class KafkaConsumerConfig : ConsumerConfig
{
    public string Topic { get; }
    
    public KafkaConsumerConfig(string topic, string groupId, string bootstrapServers)
    {
        AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
        EnableAutoOffsetStore = false;
        BootstrapServers = bootstrapServers;
        GroupId = groupId;
        Topic = topic;
    }
}