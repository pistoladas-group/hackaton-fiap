﻿using System.Text;
using dotenv.net;

namespace Hackaton.Api.Configurations;

public static class EnvironmentVariables
{
    public static string? DatabaseConnectionString { get; private set; }
    public static string StorageAccountUrl { get; private set; } = string.Empty;
    public static string StorageAccountContainerName { get; private set; } = string.Empty;

    public static IServiceCollection AddEnvironmentVariables(this IServiceCollection services, IWebHostEnvironment environment)
    {
        try
        {
            DotEnv.Fluent()
                .WithExceptions()
                .WithEnvFiles()
                .WithTrimValues()
                .WithEncoding(Encoding.UTF8)
                .WithOverwriteExistingVars()
                .WithProbeForEnv(probeLevelsToSearch: 6)
                .Load();
        }
        catch (Exception)
        {
            if (environment.IsEnvironment("Local"))
            {
                throw new ApplicationException("Environment File (.env) not found. The application needs a .env file to run locally.\nPlease check the section Environment Variables of the README.");
            }

            // Ignored if other environments because it is using runtime environment variables
        }

        LoadVariables();

        return services;
    }

    private static void LoadVariables()
    {
        DatabaseConnectionString = Environment.GetEnvironmentVariable("HACKATON_API_DATABASE_CONNECTION_STRING");
        StorageAccountUrl = Environment.GetEnvironmentVariable("TECHBOX_API_AZURE_STORAGE_ACCOUNT_URL");
        StorageAccountContainerName = Environment.GetEnvironmentVariable("TECHBOX_API_AZURE_STORAGE_ACCOUNT_CONTAINER_NAME");
    }
}
