using Microsoft.Extensions.DependencyInjection;

namespace EasyExtensions.Polly.Cache
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder CachePerUriAndMethod(this IHttpClientBuilder httpClientBuilder)
        {
            return httpClientBuilder.AddHttpMessageHandler<PerUriAndMethodContextSetter>();
        }
    }
}
