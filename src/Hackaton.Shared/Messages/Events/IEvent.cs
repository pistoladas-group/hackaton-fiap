namespace Hackaton.Shared.Messages.Events;

public interface IEvent
{
    public string GetStreamName();
}