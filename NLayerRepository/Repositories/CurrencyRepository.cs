using NLayerCore.Entities;
using NLayerCore.Interfaces;
using NLayerRepository.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerRepository.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly RedisDbContext _context;

        public CurrencyRepository(RedisDbContext context)
        {
            _context = context;
        }

        public async Task SaveCurrencyRateAsync(CurrencyRate rate)
        {
            var key = $"currency:{rate.CurrencyCode}:{rate.Date:yyyy-MM-dd}";
            var value = rate.Rate.ToString();
            await _context.Database.StringSetAsync(key, value);
        }

        public async Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync()
        {
            // Redis'ten verileri çekme mantığı buraya eklenecek.
            return new List<CurrencyRate>(); // Sadece bir şablon
        }
    }
}
