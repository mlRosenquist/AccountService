using AccountService.EventHandlers.Factories;
using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Repository.AccountRepository;
using Moq;

namespace AccountServiceTest.EventHandlers.Factories;

public class AccountEventHandlerFactoryTest
{
    private readonly AccountEventHandlerFactory _uut;
    
    public AccountEventHandlerFactoryTest()
    {
        Mock<ILogger> loggerMock = new();
        Mock<IAccountRepository> accountRepositoryMock = new();
        _uut = new AccountEventHandlerFactory(loggerMock.Object, accountRepositoryMock.Object);
    }
    
    [Fact]
    public void Create_AccountOpenedEventHandler()
    {
        var @event = new AccountOpened();
        var result = _uut.GetEventHandler(@event);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void Create_AccountClosedEventHandler()
    {
        var @event = new AccountClosed();
        var result = _uut.GetEventHandler(@event);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void Create_AccountUpdatedEventHandler()
    {
        var @event = new AccountUpdated();
        var result = _uut.GetEventHandler(@event);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void Create_AccountSettledEventHandler()
    {
        var @event = new AccountSettled();
        var result = _uut.GetEventHandler(@event);
        Assert.NotNull(result);
    }
}