using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerCore.Entities
{
    public class CurrencyRate
    {
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
