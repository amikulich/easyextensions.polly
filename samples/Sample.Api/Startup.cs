using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasyExtensions.Polly.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using Polly.Registry;
using Sample.Api.Client;

namespace Sample.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IAsyncCacheProvider, MemoryCacheProvider>();
            services.AddContextSetters();
            services.AddPolicyRegistry();

            services.AddHttpClient<SampleApiClient>()
                .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://github.com/amikulich");
            })
                .CachePerUriAndMethod()
                .AddPolicyHandlerFromRegistry((registry, request) => registry.GetCachePolicyFor<SampleApiClient>());

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            IPolicyRegistry<string> policyRegistry,
            IAsyncCacheProvider cacheProvider
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            policyRegistry.AddCachePolicyFor<SampleApiClient>(cacheProvider, TimeSpan.FromSeconds(60));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
