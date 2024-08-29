
using StackExchange.Redis;
using NLayerCore.Entities;

using NLayerCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerService.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IExchangeRateService _exchangeRateService;

        public CurrencyService(ICurrencyRepository currencyRepository, IMessageBroker messageBroker, IExchangeRateService exchangeRateService)
        {
            _currencyRepository = currencyRepository;
            _messageBroker = messageBroker;
            _exchangeRateService = exchangeRateService;
        }

        public async Task ProcessCurrencyRatesAsync()
        {
            var currencies = new[] { "USD", "EUR" }; // Gerekli döviz kurlarını buraya ekleyebilirsiniz
            var currencyRates = new List<CurrencyRate>();

            foreach (var currency in currencies)
            {
                var rate = await _exchangeRateService.GetExchangeRateAsync(currency);
                currencyRates.Add(rate);
            }

            foreach (var rate in currencyRates)
            {
                await _currencyRepository.SaveCurrencyRateAsync(rate);
            }
        }

        public async Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync()
        {
            return await _currencyRepository.GetCurrencyRatesAsync();
        }
    }
}
