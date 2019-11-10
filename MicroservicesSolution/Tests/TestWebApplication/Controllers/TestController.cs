using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using TestWebApplication.ApiClients;
using TestWebApplication.Configuration;

namespace TestWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly MicroservicesConfig _cofing;

        public TestController(ILogger<TestController> logger, IOptions<MicroservicesConfig> microConfig)
        {
            _logger = logger;
            _cofing = microConfig.Value;
        }

        [HttpGet]
        public async Task<ActionResult<Currency>> Get(string currencyCode)
        {
            _logger.LogInformation($"Get Invoked: {nameof(currencyCode)} = {currencyCode}");
            var restApi = RestService.For<ICurrencyApi>(_cofing.CurrencyUrl);
            var result = await restApi.GetAllCurrencies();
            var currency = result.FirstOrDefault(x => x.CurrencyCode == currencyCode);
            if(currency == null)
            {
                _logger.LogWarning("Warning.....");
                _logger.LogError(new ArgumentNullException(nameof(currencyCode)),
                    $"Get Error B{nameof(currencyCode)} = {currencyCode}");
                return NotFound();
            }
            _logger.LogInformation($"Finished: {nameof(currencyCode)} = {currencyCode}");
            return Ok(currency);
        }
    }
}
