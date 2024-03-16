using System.Text.Json;

namespace Hackaton.Shared.Messages.Events;

public class NewVideoEvent(Guid videoId) : IEvent
{
    public Guid VideoId { get; init; } = videoId;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}