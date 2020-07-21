using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Api.Client;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly SampleApiClient _sampleApiClient;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

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
