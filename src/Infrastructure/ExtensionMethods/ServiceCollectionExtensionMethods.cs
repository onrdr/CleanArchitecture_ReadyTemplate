using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using System.Reflection;  

namespace Infrastructure.ExtensionMethods;

public static class ServiceCollectionExtensionMethods
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services
            .ConfigureDatabase(configurationManager)
            .AddApplicationServices()
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    static IServiceCollection ConfigureDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString(nameof(ApplicationDbContext))));

        return services;
    }

    static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICategoryService, CategoryService>();

        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductService, ProductService>();

        return services;
    }
}
