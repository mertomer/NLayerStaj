using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerInfrastructure.Configration
{
    public class RabbitMqConfig
    {
        public IConnection GetRabbitMqConnection()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            return factory.CreateConnection();
        }
    }
}
