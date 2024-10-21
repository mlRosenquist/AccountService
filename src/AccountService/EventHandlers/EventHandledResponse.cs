using AccountService.EventMetadata;
using AccountService.Models;

namespace AccountService.EventHandlers;

public sealed record EventHandledResponse(bool Success, Event<Account> Event)
{
    public override string ToString() {
        return (Success ? "SUCCESS" : "FAILED") + $", Event: {Event}";
    }
}