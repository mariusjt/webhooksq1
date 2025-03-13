using System.Text.Json;
using Events;

namespace Consumer;

public class EventHandler(UserStore userStore)
{
    public async Task HandleEvent(Event @event)
    {
        switch (@event.EventType)
        {
            case nameof(UserCreated):
                var userCreated = JsonSerializer.Deserialize<UserCreated>(@event.Json);
                if (userCreated is null)
                {
                    throw new ArgumentNullException(nameof(userCreated));
                }
                
                userStore.CreateUser(new(){Id = userCreated.UserId, Name = userCreated.Name});
                break;
            case nameof(UserUpdated):
                var userUpdated = JsonSerializer.Deserialize<UserUpdated>(@event.Json);
                if (userUpdated is null)
                {
                    throw new ArgumentNullException(nameof(userUpdated));
                }
                userStore.UpdateUser(new(){Id = userUpdated.UserId, Name = userUpdated.Name});
                break;
            case nameof(UserDeleted):
                var userDeleted = JsonSerializer.Deserialize<UserDeleted>(@event.Json);
                if (userDeleted is null)
                {
                    throw new ArgumentNullException(nameof(userDeleted));
                }
                userStore.DeleteUser(userDeleted.UserId);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}