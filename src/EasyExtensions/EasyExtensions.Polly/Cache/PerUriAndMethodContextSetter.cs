using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace EasyExtensions.Polly.Cache
{
    internal class PerUriAndMethodContextSetter : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.SetPolicyExecutionContext(new Context($"{request.Method}+{request.RequestUri}"));

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
