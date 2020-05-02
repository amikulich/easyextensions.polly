using System;
using System.Net.Http;
using Polly;
using Polly.Registry;
using Polly.Timeout;

namespace EasyExtensions.Polly
{
    public static class PolicyRegistryExtensions
    {
        public static string RetryPolicyName = "RetryPolicy";
        public static string TimeoutPolicyName = "TimeoutPolicy";

        public static IPolicyRegistry<string> AddRetryPolicy(this IPolicyRegistry<string> policyRegistry, int retryCount, string prefix = "")
        {
            IAsyncPolicy<HttpResponseMessage> simpleRetryPolicy = 
                Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .WaitAndRetryAsync(retryCount, r => TimeSpan.FromSeconds(r));

            policyRegistry.Add($"{prefix}:{RetryPolicyName}", simpleRetryPolicy);

            return policyRegistry;
        }

        public static IPolicyRegistry<string> AddTimeoutPolicy(this IPolicyRegistry<string> policyRegistry, int timeoutSeconds, string prefix = "")
        {
            var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(timeoutSeconds), TimeoutStrategy.Pessimistic);
            policyRegistry.Add($"{prefix}:{TimeoutPolicyName}", timeoutPolicy);

            return policyRegistry;
        }

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(this IReadOnlyPolicyRegistry<string> policyRegistry, string prefix = "")
        {
            var policy = policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>($"{prefix}:{RetryPolicyName}");

            return policy;
        }

        public static AsyncTimeoutPolicy GetTimeoutPolicy(this IReadOnlyPolicyRegistry<string> policyRegistry, string prefix = "")
        {
            var policy = policyRegistry.Get<AsyncTimeoutPolicy>($"{prefix}:{TimeoutPolicyName}");

            return policy;
        }
    }
}
