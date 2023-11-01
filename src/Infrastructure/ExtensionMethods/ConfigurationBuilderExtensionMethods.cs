using Microsoft.Extensions.Configuration;

namespace Infrastructure.ExtensionMethods;

public static class ConfigurationBuilderExtensionMethods
{
    public static IConfigurationBuilder AddAppSettings(
        this IConfigurationBuilder configurationBuilder, string? environmentName = null, bool setBasePath = true)
    {
        if (setBasePath)
        {
            configurationBuilder
                .SetBasePath(AppContext.BaseDirectory);
        }

        configurationBuilder.AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        if (string.IsNullOrEmpty(environmentName))
        {
            environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        if (!string.IsNullOrEmpty(environmentName))
        {
            configurationBuilder
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
        }

        return configurationBuilder;
    }
}
