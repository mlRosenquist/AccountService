using AccountService.Models;
using AccountService.Repository.AccountRepository;

namespace AccountServiceTest.Repository;

public class InMemoryAccountRepositoryTest
{
    private readonly InMemoryAccountRepository _uut = new();
    
    [Fact]
    public void GetAll_WhenNoAccounts_ReturnsEmptyList()
    {
        var result = _uut.GetAll();
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetById_WhenNoAccount_ReturnsNull()
    {
        var result = _uut.GetById(1);
        Assert.Null(result);
    }
    
    [Fact]
    public void Add_WhenAccountIsAdded_ReturnsAccount()
    {
        var account = new Account { Id = 1, Name = "Test" };
        _uut.Add(account);
        
        var result = _uut.GetById(1);
        Assert.Equal(account, result);
    }
    
    [Fact]
    public void Update_WhenAccountIsUpdated_ReturnsUpdatedAccount()
    {
        var account = new Account { Id = 1, Name = "Test", Status = AccountStatus.Open};
        _uut.Add(account);
        
        const string newName = "Test 2";
        const AccountStatus newStatus = AccountStatus.Settled;
        var updatedAccount = new Account { Id = 1, Name = newName, Status = newStatus };
        _uut.Update(updatedAccount);
        
        var result = _uut.GetById(1);
        Assert.NotNull(result);
        Assert.Equal(newName, result.Name);
        Assert.Equal(newStatus, result.Status);
    }
}