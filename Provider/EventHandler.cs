using System.Text.Json;
using Events;

namespace Provider;

public class EventHandler(SubscriberStore store, HttpClient client)
{
    public async Task HandleEvent(IEvent @event)
    {
        var subscribers = store.GetSubscribers();
        foreach (var subscriber in subscribers)
        {
            await SendEvent(subscriber, @event);
        }
    }

    private async Task SendEvent(Subscriber subscriber, IEvent @event)
    {
        var payload = new Event(@event.EventType, JsonSerializer.Serialize(@event, @event.GetType()));
        await client.PostAsJsonAsync(subscriber.CallbackUrl, payload);
    }
}