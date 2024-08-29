using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerRepository.DbContext
{
    public class RedisDbContext
    {
        private readonly IConnectionMultiplexer _connection;

        public RedisDbContext(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        public IDatabase Database => _connection.GetDatabase();
    }
}
