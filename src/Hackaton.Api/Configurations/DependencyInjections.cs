using Hackaton.Api.Services;

namespace Hackaton.Api.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
    {
        services.AddSingleton<IRemoteFileStorageService, AzureRemoteFileStorageService>();
        services.AddSingleton<ILocalFileStorageService, LocalFileStorageService>();

        return services;
    }
}
