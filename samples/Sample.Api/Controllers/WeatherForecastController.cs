using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Client;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly SampleApiClient _sampleApiClient;

        public WeatherForecastController(SampleApiClient sampleApiClient)
        {
            _sampleApiClient = sampleApiClient;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await _sampleApiClient.Get();
        }
    }
}
