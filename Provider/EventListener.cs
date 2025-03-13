using System.ComponentModel;

namespace Provider;

public class EventListener(EventBus eventBus, EventHandler eventHandler) : BackgroundWorker
{
    public async Task DoWork()
    {
        while (true)
        {
            // Console.WriteLine("DoWork");
            var @event = eventBus.GetEvent();
            if (@event is not null)
            {
                await eventHandler.HandleEvent(@event);
            }
            else
            {
                await Task.Delay(250);
            }
        }
    }
}