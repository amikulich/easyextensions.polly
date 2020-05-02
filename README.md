# easyextensions.polly
A set of predefined most-common Polly policies

Register policies in a startup class:

    public void ConfigureServices(IServiceCollection services)
    {
        // other configurations
        services
            .AddPolicyRegistry()
            .AddRetryPolicy(configuration.GetValue<int>("MyConfig:RetryCount"))
            .AddTimeoutPolicy(configuration.GetValue<int>("MyConfig:TimeoutSeconds"));
    }
                
Usage example:

    private static IAsyncPolicy<HttpResponseMessage> PolicySelector(IReadOnlyPolicyRegistry<string> registry,
            HttpRequestMessage message)
    {
        var simpleRetry = registry.GetRetryPolicy();
        var timeoutPolicy = registry.GetTimeoutPolicy();
        
        return simpleRetry.WrapAsync(timeoutPolicy);
    }
