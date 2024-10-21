using AccountService.EventHandlers;
using AccountService.EventMetadata;
using AccountService.Loggers;
using AccountService.Models;
using AccountService.Repository.AccountRepository;
using Moq;

namespace AccountServiceTest.EventHandlers;

public class AccountSettledEventHandlerTest
{
    private readonly AccountSettledEventHandler _uut;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    
    private const int OpenAccountId = 1;
    private const int NonExistentAccountId = 2;
    
    public AccountSettledEventHandlerTest()
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
        _accountRepositoryMock.Setup(m => m.GetById(NonExistentAccountId))
            .Returns((Account)null);
        
        _uut = new AccountSettledEventHandler(loggerMock.Object, _accountRepositoryMock.Object);
    }
    
    [Fact]
    public void HandleEvent_StandardData_Success()
    {
        var @event = new AccountSettled
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = OpenAccountId,
                Name = "Test",
                Status = AccountStatus.Settled
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.True(result.Success);
        _accountRepositoryMock.Verify(m => m.Update(It.IsAny<Account>()), Times.Once);
    }
    
    [Fact]
    public void HandleEvent_NonExistentAccount_Failed()
    {
        var @event = new AccountSettled
        {
            Payload = new Account
            {
                Currency = "USD",
                Id = NonExistentAccountId,
                Name = "Test",
                Status = AccountStatus.Settled
            },
            Timestamp = DateTime.Now
        };
        var result = _uut.HandleEvent(@event);
        
        Assert.False(result.Success);
    }
}