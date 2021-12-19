namespace Shared.Common.Kafka;

public class KafkaConsumerConfiguration
{
    public string Topic { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string BootstrapServers { get; set; } = string.Empty;
}