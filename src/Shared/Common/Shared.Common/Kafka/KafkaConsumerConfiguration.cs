namespace Shared.Common.Kafka;

public class ConsumerConfiguration
{
    public string Topic { get; set; }
    public string GroupId { get; set; }
    public string BootstrapServers { get; set; }
}