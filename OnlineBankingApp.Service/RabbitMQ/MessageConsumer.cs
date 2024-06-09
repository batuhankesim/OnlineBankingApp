using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Service.RabbitMQ
{
    public class MessageConsumer
    {
        private readonly IConnection _connection;
        private readonly string _queueName;

        public MessageConsumer(IConfiguration configuration)
        {
            var factory = new ConnectionFactory() { HostName = configuration["RabbitMQ:HostName"] };
            _connection = factory.CreateConnection();
            _queueName = configuration["RabbitMQ:QueueName"];
        }

        public void StartListening()
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: {0}", message);
            };
            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }


    }
}
