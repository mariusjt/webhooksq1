using System.Collections.Concurrent;
using Events;

namespace Provider;

public class EventBus
{
    private readonly ConcurrentQueue<IEvent> _queue = new();

    public void AddEvent(IEvent @event)
    {
        _queue.Enqueue(@event);
    }
    
    public IEvent? GetEvent()
    {
        if (_queue.TryDequeue(out var @event))
        {
            return @event;
        }

        return null;
    }
    
    public bool HasEvent()
    {
        return _queue.Count > 0;
    }
}