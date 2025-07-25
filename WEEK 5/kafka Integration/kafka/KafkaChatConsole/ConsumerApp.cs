
using System;
using Confluent.Kafka;

class ConsumerApp
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "chat-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("chat-topic");

        Console.WriteLine("Listening for messages...");
        while (true)
        {
            var cr = consumer.Consume();
            Console.WriteLine(cr.Message.Value);
        }
    }
}
