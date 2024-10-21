using AccountService.EventHandlers;
using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.Repository.AccountRepository;
using Moq;

namespace AccountServiceTest.EventHandlers;

public class AccountClosedEventHandlerTest
{
    private readonly AccountClosedEventHandler _uut;
    private const int OpenAccountId = 1;
    private const int SettledAccountId = 2;
    private const int ClosedAccountId = 3;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    
    public AccountClosedEventHandlerTest()
    {
        Mock<ILogger> loggerMock = new();
        _accountRepositoryMock = new();
        _accountRepositoryMock.Setup(m => m.GetById(OpenAccountId))
            .Returns(new Account()
            {
                Currency = "USD",
                Id = OpenAccountId,
                Name = "Test1",
                Status = AccountStatus.Open
            });
        _accountRepositoryMock.Setup(m => m.GetById(SettledAccountId))
            .Returns(new Account()
            {
                Currency = "USD",
                Id = SettledAccountId,
                Name = "Test2",
                Status = AccountStatus.Settled
            });
        _accountRepositoryMock.Setup(m => m.GetById(ClosedAccountId))
            .Returns(new Account()
            {
                Currency = "USD",
                Id = ClosedAccountId,
                Name = "Test3",
                Status = AccountStatus.Closed
            });
            
        _uut = new AccountClosedEventHandler(loggerMock.Object, _accountRepositoryMock.Object);
    }
    
    [Fact]
    public void HandleEvent_OpenAccount_ShouldFail()
    {
        var @event = new AccountClosed
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = OpenAccountId,
                Name = "Test1",
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.False(result.Success);
    }
    
    [Fact]
    public void HandleEvent_SettledAccount_ShouldSucceed()
    {
        var @event = new AccountClosed
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = SettledAccountId,
                Name = "Test2",
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.True(result.Success);
        _accountRepositoryMock.Verify(m => m.Update(It.IsAny<Account>()), Times.Once);
    }
    
    [Fact]
    public void HandleEvent_ClosedAccount_ShouldFail()
    {
        var @event = new AccountClosed
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = ClosedAccountId,
                Name = "Test3",
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.False(result.Success);
    }
}