namespace Provider;

public class SubscriberStore
{
    private readonly List<Subscriber> _subscribers = [new("https://localhost:7136/event")];
    public List<Subscriber> GetSubscribers() => _subscribers;
    public Subscriber AddSubscriber(Subscriber subscriber)
    {
        _subscribers.Add(subscriber);
        return subscriber;
    }
    public void RemoveSubscriber(Subscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }
}

public record Subscriber(string CallbackUrl);