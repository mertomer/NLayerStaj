using NLayerCore.Entities;
using StackExchange.Redis;
using NLayerCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NLayerService.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private const string Url = "https://www.tcmb.gov.tr/kurlar/today.xml";

        public async Task<CurrencyRate> GetExchangeRateAsync(string currencyCode)
        {
            var xmlDocument = new XmlDocument();

            using (var httpClient = new HttpClient())
            {
                var xmlContent = await httpClient.GetStringAsync(Url);
                xmlDocument.LoadXml(xmlContent);
            }

            var buyingRateNode = xmlDocument.SelectSingleNode($"Tarih_Date/Currency[@Kod='{currencyCode}']/BanknoteBuying");
            var sellingRateNode = xmlDocument.SelectSingleNode($"Tarih_Date/Currency[@Kod='{currencyCode}']/BanknoteSelling");

            if (buyingRateNode == null || sellingRateNode == null)
            {
                throw new Exception($"Currency code '{currencyCode}' not found.");
            }

            return new CurrencyRate
            {
                CurrencyCode = currencyCode,
                Rate = decimal.Parse(buyingRateNode.InnerXml),
                Date = DateTime.Now
            };
        }
    }
}
