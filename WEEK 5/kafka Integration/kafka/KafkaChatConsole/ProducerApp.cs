
using System;
using Confluent.Kafka;
using System.Threading.Tasks;

class ProducerApp
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter your name:");
        var user = Console.ReadLine();

        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        Console.WriteLine("Start typing messages...");
        while (true)
        {
            var message = Console.ReadLine();
            var fullMessage = $"{user}: {message}";

            await producer.ProduceAsync("chat-topic", new Message<Null, string> { Value = fullMessage });
            Console.WriteLine("Sent: " + fullMessage);
        }
    }
}
