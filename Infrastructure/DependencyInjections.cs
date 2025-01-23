using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<RealDatabase>();
            
            services.AddDbContext<RealDatabase>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
