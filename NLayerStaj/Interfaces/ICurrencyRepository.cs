using NLayerCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerCore.Interfaces
{
    public interface ICurrencyRepository
    {
        Task SaveCurrencyRateAsync(CurrencyRate rate);
        Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync();
    }
}
