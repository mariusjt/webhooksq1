namespace Events;

public interface IEvent { string EventType { get; } }

public record Event(string EventType, string Json);
public record UserCreated(int UserId, string Name) : IEvent
{
    public string EventType => nameof(UserCreated);
}

public record UserUpdated(int UserId, string Name): IEvent
{
    public string EventType => nameof(UserUpdated);
}

public record UserDeleted(int UserId) : IEvent
{
    public string EventType => nameof(UserDeleted);
}