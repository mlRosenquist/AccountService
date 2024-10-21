using AccountService.Models;

namespace AccountService.EventMetadata;

public sealed record AccountOpened : Event<Account>;