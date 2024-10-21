using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.Repository.AccountRepository;

namespace AccountService.EventHandlers.Factories;


public class AccountEventHandlerFactory(ILogger logger, IAccountRepository repository) : IEventHandlerFactory
{
    public IAccountEventHandler GetEventHandler<T>(T @event) where T : Event<Account>
    {
        return @event switch
        {
            AccountUpdated => new AccountUpdatedEventHandler(logger, repository),
            AccountOpened => new AccountOpenedEventHandler(logger, repository),
            AccountSettled => new AccountSettledEventHandler(logger, repository),
            AccountClosed => new AccountClosedEventHandler(logger, repository),
            _ => throw new Exception("Event type not supported")
        };
    }
}