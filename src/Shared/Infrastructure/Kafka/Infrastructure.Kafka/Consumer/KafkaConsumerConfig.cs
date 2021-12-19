﻿using Confluent.Kafka;

namespace Infrastructure.Kafka.Consumer;

public class KafkaConsumerConfig<Tk, Tv> : ConsumerConfig
{
    public string Topic { get; set; }
    public KafkaConsumerConfig()
    {
        AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
        EnableAutoOffsetStore = false;
    }
}