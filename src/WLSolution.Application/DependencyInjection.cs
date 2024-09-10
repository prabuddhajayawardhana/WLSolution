using Microsoft.Extensions.DependencyInjection;
using WLSolution.Application.Interfaces;
using WLSolution.Application.Services;

namespace WLSolution.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}