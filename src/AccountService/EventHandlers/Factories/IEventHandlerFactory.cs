using AccountService.EventMetadata;
using AccountService.Models;

namespace AccountService.EventHandlers.Factories;

public interface IEventHandlerFactory
{
    public IAccountEventHandler GetEventHandler<T>(T @event) where T : Event<Account>;
}