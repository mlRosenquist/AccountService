using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.Repository.AccountRepository;

namespace AccountService.EventHandlers;

public class AccountUpdatedEventHandler(ILogger logger, IAccountRepository repository)
    : AccountEventHandler(logger, repository)
{

    protected override EventHandledResponse ProcessEvent(Event<Account> @event)
    {
        var account = Repository.GetById(@event.Payload.Id);
        if (account == null)
        {
            return new EventHandledResponse(false, @event);
        }
        
        Repository.Update(new Account()
        {
            Id = @event.Payload.Id,
            Name = @event.Payload.Name,
            Status = account.Status
        });
        
        return new EventHandledResponse(true, @event);
    }
}