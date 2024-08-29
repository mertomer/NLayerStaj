using NLayerCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerCore.Interfaces
{
    public interface IExchangeRateService
    {
        Task<CurrencyRate> GetExchangeRateAsync(string currencyCode);
    }
}
