using AccountService.Models;
using System.Collections.Concurrent;

namespace AccountService.Repository.AccountRepository;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly ConcurrentDictionary<int, Account> _accounts = new();

    public Account? GetById(int id)
    {
        _accounts.TryGetValue(id, out var account);
        return account;
    }

    public IEnumerable<Account> GetAll()
    {
        return _accounts.Values;
    }

    public void Add(Account account)
    {
        _accounts[account.Id] = account;
    }

    public void Update(Account account)
    {
        _accounts[account.Id] = account;
    }
}