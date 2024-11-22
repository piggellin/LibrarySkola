using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjections).Assembly;
            services.AddMediatR(confirguration => confirguration.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
