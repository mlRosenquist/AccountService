using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.Repository.AccountRepository;

namespace AccountService.EventHandlers;

public sealed class AccountOpenedEventHandler(ILogger logger, IAccountRepository repository)
    : AccountEventHandler(logger, repository)
{
    protected override EventHandledResponse ProcessEvent(Event<Account> @event)
    {
        Repository.Add(new Account
        {
            Id = @event.Payload.Id,
            Name = @event.Payload.Name,
            Status = AccountStatus.Open
        });
        return new EventHandledResponse(true, @event);
    }
}