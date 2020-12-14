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
            // the very first call will hit https://github.com/amikulich. The further calls will take data from cache.
            var response = await _httpClient.GetAsync("amikulich");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
