using System;
using Hackaton.Shared.Messages.Events;

namespace Hackaton.Shared.MessageBus;

public interface IMessageBus
{
    public void Publish<T>(T message) where T : IEvent;
    public void Consume<T>(string queueName, Action<T?> executeAfterConsumed) where T : IEvent;
}