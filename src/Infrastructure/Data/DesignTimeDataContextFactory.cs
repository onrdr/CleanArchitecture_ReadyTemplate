using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.ExtensionMethods;

namespace Infrastructure.Data;

internal class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddAppSettings(setBasePath: false)
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)));

        return new ApplicationDbContext(builder.Options);
    }
}
