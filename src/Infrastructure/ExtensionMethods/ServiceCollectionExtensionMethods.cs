using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using DataAccess.Repositories.Concrete.Cache;
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
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddMemoryCache();

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
        services.AddScoped<CategoryRepository>();
        services.AddScoped<ICategoryRepository, CachedCategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<ProductRepository>();
        services.AddScoped<IProductRepository, CachedProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
