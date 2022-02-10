using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Persistance;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepositoryRedis, OrderRepositoryRedis>();
            return services;
        }       

    }
}
