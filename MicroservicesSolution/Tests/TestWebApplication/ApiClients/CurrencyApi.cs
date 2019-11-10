using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApplication.ApiClients
{
    public interface ICurrencyApi
    {
        [Get("/currency")]
        public Task<List<Currency>> GetAllCurrencies();
    }

    public class Currency
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public string CurrencyCode { get; set; }
        public double Change { get; set; }
        public string Country { get; set; }
    }
}
