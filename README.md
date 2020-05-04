# easyextensions.polly
A set of predefined most-common Polly policies

Register policies in a startup class:

    public void ConfigureServices(IServiceCollection services)
    {
        // other configurations               
        services
            .AddPolicyRegistry()
            .AddRetryPolicy(5)
            .AddTimeoutRetryAndWaitPolicy(3, 30);
    }
                
Usage example:

    private static IAsyncPolicy<HttpResponseMessage> PolicySelector(IReadOnlyPolicyRegistry<string> registry,
            HttpRequestMessage message)
    {
        if (message.Method == HttpMethod.Get)
        {
            return registry.GetTimeoutRetryAndWaitPolicy();
        }
        
        return registry.GetRetryPolicy();;
    }
