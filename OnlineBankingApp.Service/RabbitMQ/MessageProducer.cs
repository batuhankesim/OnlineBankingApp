using RabbitMQ.Client;
using System.Text;

namespace OnlineBankingApp.Service.RabbitMQ
{
    public class MessageProducer
    {
        private readonly IConnection _connection;
        private readonly string _queueName;

        public MessageProducer(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };
            _connection = factory.CreateConnection();
            _queueName = configuration["RabbitMQ:QueueName"];
        }

        public void SendMessage(string message)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }

    }
}
