
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;

namespace KafkaChatWinForms
{
    public partial class Form1 : Form
    {
        private string username;
        public Form1()
        {
            InitializeComponent();
            Task.Run(() => StartConsumer());
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            var message = $"{username}: {txtMessage.Text}";
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            await producer.ProduceAsync("chat-topic", new Message<Null, string> { Value = message });

            txtMessage.Clear();
        }

        private void StartConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "winform-chat-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("chat-topic");

            while (true)
            {
                var msg = consumer.Consume();
                Invoke(new Action(() => txtChat.AppendText(msg.Message.Value + Environment.NewLine)));
            }
        }
    }
}
