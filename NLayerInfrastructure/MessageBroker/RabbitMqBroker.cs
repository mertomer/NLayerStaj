using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using NLayerCore.Interfaces;

namespace NLayerInfrastructure.MessageBroker
{
    public class RabbitMqBroker : IMessageBroker
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqBroker(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
        }

        public async Task<IEnumerable<T>> ReceiveAsync<T>()
        {
            var messageList = new List<T>();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonSerializer.Deserialize<T>(message);
                if (data != null)
                {
                    messageList.Add(data);
                }
            };
            _channel.BasicConsume(queue: "currency_rate_queue", autoAck: true, consumer: consumer);
            await Task.Delay(1000);  // Mesajların tamamlanmasını beklemek için kısa bir gecikme
            return messageList;
        }
    }
}
