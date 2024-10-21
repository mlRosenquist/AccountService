using System.Collections.Concurrent;
using AccountService.EventHandlers.Factories;
using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.RetryPolicy;

namespace AccountService;

public sealed class AccountService(string podName, ILogger logger, IEventHandlerFactory eventHandlerFactory, IRetryPolicy retryPolicy)
{
    public string PodName => podName;
    private readonly ConcurrentQueue<Event<Account>> _events = new();
    private bool _running;
    
    public void Start()
    {
        logger.Log("Started");
        _running = true;
        Run();
    }
    
    public void Stop()
    {
        logger.Log("Stopping");
        _running = false;
    }

    public void AddEvent(Event<Account> @event)
    {
        _events.Enqueue(@event);
    }
    
    private bool HandleEvent(Event<Account> @event)
    {
        var eventHandler = eventHandlerFactory.GetEventHandler(@event);
        var response = eventHandler.HandleEvent(@event);
        return response.Success;
    }

    private void Run()
    {
        while (_running)
        {
            var anyEvents = _events.TryDequeue(out var @event);
            if(!anyEvents || @event == null) continue;

            // Skip events that are awaiting retry policy
            if (@event.Retry != null && @event.Retry?.NextAttempt > DateTime.UtcNow)
            {
                _events.Enqueue(@event);
                continue;
            }

            var eventSuccess = HandleEvent(@event);
            if (eventSuccess) continue;
            
            ApplyRetryPolicy(@event);
        }
    }

    private void ApplyRetryPolicy(Event<Account> @event)
    {
        @event.Retry = retryPolicy.GetNextRetry(@event.Retry);
        logger.Log($"Will retry after {@event.Retry.LastDelayInMs}ms");
        
        _events.Enqueue(@event);
    }
}