using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerCore.Interfaces;

namespace NLayerDovizAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("process")]
        public async Task<IActionResult> ProcessCurrencyRates()
        {
            await _currencyService.ProcessCurrencyRatesAsync();
            return Ok("Currency rates processed and saved.");
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencyRates()
        {
            var rates = await _currencyService.GetCurrencyRatesAsync();
            return Ok(rates);
        }
    }
}
