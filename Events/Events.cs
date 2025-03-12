namespace Events;

public record UserCreated(int UserId, string Name);
public record UserUpdated(int UserId, string Name);
public record UserDeleted(int UserId, string Name);