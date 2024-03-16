using Hackaton.Shared.MessageBus;
using Hackaton.Shared.Messages.Events;
using Hackaton.Worker.Configurations;
using Serilog;

namespace Hackaton.Worker;

public class Worker(IMessageBus bus) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Worker has started");
        bus.Consume<NewVideoEvent>(EnvironmentVariables.BrokerNewVideoToProcessQueueName, ExecuteAfterConsumed);
        return Task.CompletedTask;
    }
    
    private void ExecuteAfterConsumed(NewVideoEvent? message)
    {
        Log.Information("New message received: {@message}", message);

        if (message is null)
        {
            Log.Warning("Message is null. Skipping e-mail notification");
            return;
        }

        try
        {
            //TODO: do stuff
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while sending notification");
            throw;
        }
    }
}
