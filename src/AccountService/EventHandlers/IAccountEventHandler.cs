using AccountService.EventMetadata;
using AccountService.Models;

namespace AccountService.EventHandlers;

public interface IAccountEventHandler
{
    public EventHandledResponse HandleEvent(Event<Account> @event);
}