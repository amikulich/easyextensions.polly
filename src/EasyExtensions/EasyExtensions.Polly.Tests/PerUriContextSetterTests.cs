using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EasyExtensions.Polly.Cache;
using NUnit.Framework;
using Polly;

namespace EasyExtensions.Polly.Tests
{
    [TestFixture]
    public class PerUriContextSetterTests
    {
        private PerUriAndMethodContextSetter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new PerUriAndMethodContextSetter()
            {
                InnerHandler = new DummyHandler()
            };
        }

        [Test]
        public async Task SendAsync_Get_HappyPath()
        {
            var testUri = "https://test.com/v1/orders";
            var request = new HttpRequestMessage(HttpMethod.Get, testUri);

            var invoker = new HttpMessageInvoker(_sut);
            await invoker.SendAsync(request, new CancellationToken());

            Assert.AreEqual("GET+https://test.com/v1/orders", request.GetPolicyExecutionContext().OperationKey);
        }

        [Test]
        public async Task SendAsync_Post_HappyPath()
        {
            var testUri = "https://test.com/v1/orders";
            var request = new HttpRequestMessage(HttpMethod.Post, testUri);

            var invoker = new HttpMessageInvoker(_sut);
            await invoker.SendAsync(request, new CancellationToken());

            Assert.AreEqual("POST+https://test.com/v1/orders", request.GetPolicyExecutionContext().OperationKey);
        }


        internal class DummyHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK), cancellationToken);
            }
        }
    }
}
