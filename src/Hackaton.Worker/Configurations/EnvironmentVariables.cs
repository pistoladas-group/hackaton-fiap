using System.Text;
using dotenv.net;
using Serilog;

namespace Hackaton.Worker.Configurations;

public static class EnvironmentVariables
{
    public static string BrokerHostName { get; private set; } = string.Empty;
    public static string BrokerVirtualHost { get; private set; } = string.Empty;
    public static string BrokerUserName { get; private set; } = string.Empty;
    public static string BrokerPassword { get; private set; } = string.Empty;
    public static string? DiscordWebhookId { get; private set; }
    public static string? DiscordWebhookToken { get; private set; }
    
    public static string BrokerNewVideoToProcessQueueName { get; private set; } = string.Empty;



    public static IServiceCollection AddEnvironmentVariables(this IServiceCollection services, IHostEnvironment environment)
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
            Log.Fatal("Environment variables not defined");
        }

        LoadVariables();

        return services;
    }

    private static void LoadVariables()
    {
        BrokerNewVideoToProcessQueueName = Environment.GetEnvironmentVariable("BROKER_NEW_VIDEO_TO_PROCESS_QUEUE_NAME") ?? string.Empty;

        BrokerHostName = Environment.GetEnvironmentVariable("BROKER_HOST_NAME") ?? string.Empty;
        BrokerVirtualHost = Environment.GetEnvironmentVariable("BROKER_VIRTUAL_HOST") ?? string.Empty;
        BrokerUserName = Environment.GetEnvironmentVariable("BROKER_USER_NAME") ?? string.Empty;
        BrokerPassword = Environment.GetEnvironmentVariable("BROKER_PASSWORD") ?? string.Empty;
        
        DiscordWebhookId = Environment.GetEnvironmentVariable("DISCORD_WEBHOOK_ID");
        DiscordWebhookToken = Environment.GetEnvironmentVariable("DISCORD_WEBHOOK_TOKEN");
    }
}