using Microsoft.Extensions.DependencyInjection;
using WLSolution.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WLSolution.Domain.Interfaces;
using WLSolution.Infrastructure.Repositories;

namespace WLSolution.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SQlConection"), options => options.EnableRetryOnFailure()));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
       
        return services;
    }
}
