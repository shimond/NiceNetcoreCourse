using System.Collections.Generic;
using CurrenciesService.Model;

namespace CurrenciesService.Services
{
    public interface ICurrencyService
    {
        List<Currency> GetAll();
    }
}