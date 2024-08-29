using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerCore.Interfaces
{
    public interface IMessageBroker
    {
        Task<IEnumerable<T>> ReceiveAsync<T>();
        void Publish<T>(T message, string queueName);
    }
}
