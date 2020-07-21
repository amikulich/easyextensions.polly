# easyextensions.polly

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IAsyncCacheProvider, MemoryCacheProvider>();
        services.AddContextSetters();
        services.AddPolicyRegistry();

        services.AddHttpClient<SampleApiClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://localhost");
            })
            .CachePerUriAndMethod()
            .AddPolicyHandlerFromRegistry((registry, request) => registry.GetCachePolicyFor<SampleApiClient>());

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, 
                IWebHostEnvironment env,
                IPolicyRegistry<string> policyRegistry,
                IAsyncCacheProvider cacheProvider)
    {
        policyRegistry.AddCachePolicyFor<SampleApiClient>(cacheProvider, TimeSpan.FromSeconds(60));
        
        /* ... */
    }

