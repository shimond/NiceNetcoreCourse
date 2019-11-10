using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrenciesService.Model
{
    public class Currency
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public string CurrencyCode { get; set; }
        public double Change { get; set; }
        public string Country { get; set; }
    }

}
