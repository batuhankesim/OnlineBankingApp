using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Service.RabbitMQ
{
    public class RabbitMQConfiguration
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ExchangeName { get; set; }

        public string QueueName { get; set; }
    }
}
