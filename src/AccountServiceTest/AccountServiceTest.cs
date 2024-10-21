using AccountService.EventHandlers;
using AccountService.EventHandlers.Factories;
using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.RetryPolicy;
using Moq;

namespace AccountServiceTest;

public class AccountServiceTest
{
    private readonly AccountService.AccountService _uut;
    private readonly Mock<IEventHandlerFactory> _eventHandlerFactoryMock;
    private readonly Mock<IRetryPolicy> _retryPolicyMock;
    private readonly Mock<IAccountEventHandler> _accountEventHandlerMock;
    
    public AccountServiceTest()
    {
        Mock<ILogger> loggerMock = new();
        _accountEventHandlerMock = new();
        _accountEventHandlerMock.Setup(m => m.HandleEvent(It.IsAny<Event<Account>>()))
            .Returns(new EventHandledResponse(true, null));
        
        _eventHandlerFactoryMock = new();
        _eventHandlerFactoryMock.Setup(m => m.GetEventHandler(It.IsAny<Event<Account>>()))
            .Returns(_accountEventHandlerMock.Object);
        
        
        _retryPolicyMock = new();
        _uut = new AccountService.AccountService("uut", loggerMock.Object, _eventHandlerFactoryMock.Object, _retryPolicyMock.Object);
    }

    [Fact]
    public void Start_OneOfEachEvents_Success()
    {
        _accountEventHandlerMock.Setup(m => m.HandleEvent(It.IsAny<Event<Account>>()))
            .Returns(new EventHandledResponse(true, null));
        
        var events = new List<Event<Account>>()
        {
            new AccountOpened(),
            new AccountClosed(),
            new AccountUpdated(),
            new AccountSettled()
        };
        
        new Thread(() => _uut.Start())
            .Start();
        
        foreach (var @event in events)
        {
            _uut.AddEvent(@event);
        }
        
        Thread.Sleep(25); // yes i know...
        _uut.Stop();
        
        _eventHandlerFactoryMock.Verify(m => m.GetEventHandler(It.IsAny<Event<Account>>()), Times.Exactly(events.Count));
        _accountEventHandlerMock.Verify(m => m.HandleEvent(It.IsAny<Event<Account>>()), Times.Exactly(events.Count));
        _retryPolicyMock.Verify(m => m.GetNextRetry(It.IsAny<Retry>()), Times.Never());
    }

    [Fact]
    public void Start_TwoEvents_CloseShouldTriggerRetry()
    {
        var events = new List<Event<Account>>()
        {
            new AccountOpened(),
            new AccountClosed()
        };
        _accountEventHandlerMock.Setup(m => m.HandleEvent(It.IsAny<AccountOpened>()))
            .Returns(new EventHandledResponse(true, null));
        _accountEventHandlerMock.Setup(m => m.HandleEvent(It.IsAny<AccountClosed>()))
            .Returns(new EventHandledResponse(false, null));
        
        _retryPolicyMock.Setup(m => m.GetNextRetry(It.IsAny<Retry>()))
            .Returns(new Retry
            {
                AttemptCount = 1,
                LastAttempt = DateTime.Now,
                LastDelayInMs = 1,
                NextAttempt = DateTime.Now.AddDays(1)
            });
        
        new Thread(() => _uut.Start())
            .Start();

        foreach (var @event in events)
        {
            _uut.AddEvent(@event);
        }
        
        Thread.Sleep(25); // yes i know...
        _uut.Stop();
        
        _eventHandlerFactoryMock.Verify(m => m.GetEventHandler(It.IsAny<Event<Account>>()), Times.Exactly(events.Count));
        _accountEventHandlerMock.Verify(m => m.HandleEvent(It.IsAny<Event<Account>>()), Times.Exactly(events.Count));
        _retryPolicyMock.Verify(m => m.GetNextRetry(It.IsAny<Retry>()), Times.Once());
    }
    
    [Fact]
    public void Start_TwoEvents_CloseShouldTryMultipleHandles()
    {
        var events = new List<Event<Account>>()
        {
            new AccountOpened(),
            new AccountClosed()
        };
        _accountEventHandlerMock.Setup(m => m.HandleEvent(It.IsAny<AccountOpened>()))
            .Returns(new EventHandledResponse(true, null));
        _accountEventHandlerMock.Setup(m => m.HandleEvent(It.IsAny<AccountClosed>()))
            .Returns(new EventHandledResponse(false, null));
        
        _retryPolicyMock.Setup(m => m.GetNextRetry(It.IsAny<Retry>()))
            .Returns(new Retry
            {
                AttemptCount = 1,
                LastAttempt = DateTime.Now,
                LastDelayInMs = 1,
                NextAttempt = DateTime.Now.AddDays(-1)
            });
        
        new Thread(() => _uut.Start())
            .Start();

        foreach (var @event in events)
        {
            _uut.AddEvent(@event);
        }
        
        Thread.Sleep(25); // yes i know...
        _uut.Stop();
        
        _eventHandlerFactoryMock.Verify(m => m.GetEventHandler(It.IsAny<Event<Account>>()), Times.AtLeast(events.Count + 1));
        _accountEventHandlerMock.Verify(m => m.HandleEvent(It.IsAny<Event<Account>>()), Times.AtLeast(events.Count + 1));
        _retryPolicyMock.Verify(m => m.GetNextRetry(It.IsAny<Retry>()), Times.AtLeast(2));
    }
}