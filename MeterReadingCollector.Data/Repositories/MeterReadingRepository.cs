using MeterReadingCollector.Data.Context;
using MeterReadingCollector.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingCollector.Data.Repositories;

public class MeterReadingRepository(MeterReadingContext context) : IMeterReadingRepository
{
    public async Task<bool> MeterReadingExistsAsync(int accountId, DateTime readingDateTime)
    {
        return await context.MeterReadings
            .AnyAsync(m => m.AccountId == accountId && m.MeterReadingDateTime == readingDateTime);
    }

    public async Task<bool> AccountExistsAsync(int accountId)
    {
        return await context.Accounts.AnyAsync(a => a.AccountId == accountId);
    }

    public async Task AddAsync(MeterReading meterReading)
    {
        await context.MeterReadings.AddAsync(meterReading);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
