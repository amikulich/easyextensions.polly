using Microsoft.Extensions.DependencyInjection;

namespace EasyExtensions.Polly.Cache
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddContextSetters(this IServiceCollection services)
        {
            services.AddTransient<PerUriAndMethodContextSetter>();

            return services;
        }
    }
}
