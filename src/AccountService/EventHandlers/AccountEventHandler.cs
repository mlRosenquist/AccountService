using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.Repository.AccountRepository;

namespace AccountService.EventHandlers;

public abstract class AccountEventHandler(ILogger logger, IAccountRepository repository) : IAccountEventHandler
{
    protected readonly IAccountRepository Repository = repository;

    private bool Validate(Event<Account> @event)
    {
        return (@event.Payload.Id != default && !string.IsNullOrEmpty(@event.Payload.Name));
    }
    
    protected abstract EventHandledResponse ProcessEvent(Event<Account> @event);
    
    public EventHandledResponse HandleEvent(Event<Account> @event)
    {
        logger.Log($"Handling event {@event}");
        
        var isEventValid = Validate(@event);
        if (!isEventValid)
        {
            logger.Log($"Event {nameof(@event)} is invalid");
            return new EventHandledResponse(false, @event);
        }
        
        var response = ProcessEvent(@event);
        
        logger.Log($"Event handled with response {response}");
        return response;
    }
}