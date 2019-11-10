using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrenciesService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrenciesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<CurrencyController> _logger;

        public IActionResult Get()
        {
            _logger.LogInformation("Get all currencies (CurrencyController)");
            return Ok(_currencyService.GetAll());
        }

        public CurrencyController(ILogger<CurrencyController> logger,
                ICurrencyService currencyService
            )
        {
            _currencyService = currencyService;
            _logger = logger;
        }

    }
}
