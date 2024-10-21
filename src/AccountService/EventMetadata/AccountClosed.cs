using AccountService.Models;

namespace AccountService.EventMetadata;

public sealed record AccountClosed : Event<Account>;