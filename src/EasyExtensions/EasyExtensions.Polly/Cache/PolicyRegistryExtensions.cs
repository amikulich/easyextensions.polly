using System;
using System.Net.Http;
using Polly;
using Polly.Caching;
using Polly.Registry;

namespace EasyExtensions.Polly.Cache
{
    public static class PolicyRegistryExtensions
    {
        public const string CachePolicyPrefix = "Cache_For_";

        public static IPolicyRegistry<string> AddCachePolicyFor<T>(this IPolicyRegistry<string> policyRegistry,
            IAsyncCacheProvider cacheProvider,
            TimeSpan ttl)
        {
            Ttl TtlFilter(Context context, HttpResponseMessage response) => new Ttl(response.IsSuccessStatusCode ? ttl : TimeSpan.Zero);

            AsyncCachePolicy<HttpResponseMessage> policy = 
                Policy.CacheAsync(cacheProvider.AsyncFor<HttpResponseMessage>(), 
                    new ResultTtl<HttpResponseMessage>(TtlFilter) );

            policyRegistry.Add($"{CachePolicyPrefix}{typeof(T).FullName}", policy);

            return policyRegistry;
        }

        public static AsyncCachePolicy<HttpResponseMessage> GetCachePolicyFor<T>(this IPolicyRegistry<string> policyRegistry)
        {
            var policy = policyRegistry.Get<AsyncCachePolicy<HttpResponseMessage>>($"{CachePolicyPrefix}{typeof(T).FullName}");

            return policy;
        }

        public static AsyncCachePolicy<HttpResponseMessage> GetCachePolicyFor<T>(this IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            var policy = policyRegistry.Get<AsyncCachePolicy<HttpResponseMessage>>($"{CachePolicyPrefix}{typeof(T).FullName}");

            return policy;
        }
    } 
}
