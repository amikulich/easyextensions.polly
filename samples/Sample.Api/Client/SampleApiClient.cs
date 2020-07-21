using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sample.Api.Client
{
    public class SampleApiClient
    {
        private readonly HttpClient _httpClient;

        public SampleApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Get()
        {
            var response = await _httpClient.GetAsync("packages/Polly");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
