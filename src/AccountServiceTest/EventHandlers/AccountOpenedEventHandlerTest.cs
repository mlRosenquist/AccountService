using AccountService.EventHandlers;
using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.Repository.AccountRepository;
using Moq;

namespace AccountServiceTest.EventHandlers;

public class AccountOpenedEventHandlerTest
{
    private readonly AccountOpenedEventHandler _uut;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    
    public AccountOpenedEventHandlerTest()
    {
        Mock<ILogger> loggerMock = new();
         _accountRepositoryMock = new();
        _uut = new AccountOpenedEventHandler(loggerMock.Object, _accountRepositoryMock.Object);
    }
    
    [Fact]
    public void HandleEvent_StandardData_Success()
    {
        var @event = new AccountOpened
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = 1,
                Name = "Test",
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.True(result.Success);
        _accountRepositoryMock.Verify(m => m.Add(It.IsAny<Account>()), Times.Once);
    }
    
    [Fact]
    public void HandleEvent_InvalidId_Failed()
    {
        var @event = new AccountOpened
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = 0,
                Name = "Test",
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.False(result.Success);
    }
    
    [Fact]
    public void HandleEvent_InvalidName_Failed()
    {
        var @event = new AccountOpened
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = 1,
                Name = "",
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.False(result.Success);
    }
}