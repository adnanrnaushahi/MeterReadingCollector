using MeterReadingCollector.Data.Context;
using MeterReadingCollector.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingCollector.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly MeterReadingContext _context;

    public AccountRepository(MeterReadingContext context)
    {
        _context = context;
    }

    public async Task<bool> AccountExistsAsync(int accountId)
    {
        return await _context.Accounts.AnyAsync(a => a.AccountId == accountId);
    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
        return await _context.Accounts.FindAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }

    public async Task AddRangeAsync(IEnumerable<Account> accounts)
    {
        await _context.Accounts.AddRangeAsync(accounts);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
