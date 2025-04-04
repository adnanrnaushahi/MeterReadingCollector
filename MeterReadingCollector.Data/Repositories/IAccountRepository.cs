

using MeterReadingCollector.Data.Entities;

namespace MeterReadingCollector.Data.Repositories;

public interface IAccountRepository
{
    Task<bool> AccountExistsAsync(int accountId);
    Task<Account?> GetByIdAsync(int accountId);
    Task<IEnumerable<Account>> GetAllAsync();
    Task AddAsync(Account account);
    Task AddRangeAsync(IEnumerable<Account> accounts);
    Task<int> SaveChangesAsync();
}
