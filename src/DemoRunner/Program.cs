using AccountService.EventHandlers.Factories;
using AccountService.Loggers;
using AccountService.Repository.AccountRepository;
using AccountService.RetryPolicy;
using DemoRunner;

var accountRepository = new InMemoryAccountRepository();
var retryPolicy = new ConstantRetryPolicy(2000);

var pod1Name = "A";
var logger1 = new ConsoleLogger(pod1Name);
var accountEventsHandlerFactory1 = new AccountEventHandlerFactory(logger1, accountRepository);
var pod1 = new AccountService.AccountService(pod1Name, logger1, accountEventsHandlerFactory1, retryPolicy);

var pod2Name = "B";
var logger2 = new ConsoleLogger(pod2Name);
var accountEventsHandlerFactory2 = new AccountEventHandlerFactory(logger2, accountRepository);
var pod2 = new AccountService.AccountService(pod2Name, logger2, accountEventsHandlerFactory2, retryPolicy);

new Thread(pod1.Start)
    .Start();
new Thread(pod2.Start)
    .Start();

new Thread(() =>
{
    var random = new Random();
    foreach (var @event in EventSamples.Pod1)
    {
        Thread.Sleep(random.Next(500, 1000));
        pod1.AddEvent(@event);
    }
}).Start();

new Thread(() =>
{
    var random = new Random();
    foreach (var @event in EventSamples.Pod2)
    {
        Thread.Sleep(random.Next(500, 1000));
        pod2.AddEvent(@event);
    }
}).Start();