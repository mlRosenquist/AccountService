using AccountService.EventMetadata;
using AccountService.Models;

namespace DemoRunner;

public static class EventSamples
{
    public static List<Event<Account>> Pod1 =>
    [
        new AccountOpened
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:00Z"),
            Payload = new Account { Id = 1, Currency = "DKK", Name = "Salary" }
        },

        new AccountUpdated
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:01Z"),
            Payload = new Account { Id = 1, Currency = "DKK", Name = "Salary 2" }
        },

        new AccountClosed
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:06Z"),
            Payload = new Account { Id = 1, Currency = "DKK", Name = "Salary 2" }
        },

        new AccountOpened
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:02Z"),
            Payload = new Account { Id = 2, Currency = "EUR", Name = "Savings" }
        },
        
        new AccountSettled
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:08Z"),
            Payload = new Account { Id = 2, Currency = "GBP", Name = "My savings" }
        },
        new AccountOpened
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:10Z"),
            Payload = new Account { Id = 3, Currency = "NOK", Name = "Inpayments" }
        },
        
        new AccountClosed
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:16Z"),
            Payload = new Account { Id = 3, Currency = "NOK", Name = "MB payments" }
        },
        
        new AccountSettled
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:13Z"),
            Payload = new Account { Id = 3, Currency = "NOK", Name = "MB payments" }
        },
        
        new AccountUpdated
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:11Z"),
            Payload = new Account { Id = 4, Currency = "DKK", Name = "Salary 37" }
        },
        
        new AccountSettled
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:14Z"),
            Payload = new Account { Id = 4, Currency = "DKK", Name = "Salary 37" }
        },
        
        new AccountUpdated
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:18Z"),
            Payload = new Account { Id = 5, Currency = "USD", Name = "Salary 242" }
        },
        
        new AccountClosed
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:20Z"),
            Payload = new Account { Id = 5, Currency = "USD", Name = "Salary 242" }
        }
    ];

    public static List<Event<Account>> Pod2 =>
    [
        new AccountSettled
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:04Z"),
            Payload = new Account { Id = 1, Currency = "DKK", Name = "Salary 2" }
        },
        new AccountUpdated
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:03Z"),
            Payload = new Account { Id = 2, Currency = "GBP", Name = "Savings" }
        },
        new AccountUpdated
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:07Z"),
            Payload = new Account { Id = 2, Currency = "GBP", Name = "My savings" }
        },
        new AccountClosed
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:09Z"),
            Payload = new Account { Id = 2, Currency = "GBP", Name = "My savings" }
        },
        new AccountUpdated
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:12Z"),
            Payload = new Account { Id = 3, Currency = "NOK", Name = "MB payments" }
        },
        new AccountOpened
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:05Z"),
            Payload = new Account { Id = 4, Currency = "DKK", Name = "Salary 42" }
        },
        new AccountClosed
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:17Z"),
            Payload = new Account { Id = 4, Currency = "DKK", Name = "Salary 37" }
        },
        new AccountOpened
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:15Z"),
            Payload = new Account { Id = 5, Currency = "USD", Name = "Salary 24" }
        },
        new AccountSettled
        {
            Timestamp = DateTime.Parse("1971-03-15T08:00:19Z"),
            Payload = new Account { Id = 5, Currency = "USD", Name = "Salary 242" }
        }
    ];
}