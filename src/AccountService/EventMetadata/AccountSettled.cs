using AccountService.Models;

namespace AccountService.EventMetadata;

public sealed record AccountSettled : Event<Account>;