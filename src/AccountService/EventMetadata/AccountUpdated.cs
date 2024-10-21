using AccountService.Models;

namespace AccountService.EventMetadata;

public sealed record AccountUpdated : Event<Account>;