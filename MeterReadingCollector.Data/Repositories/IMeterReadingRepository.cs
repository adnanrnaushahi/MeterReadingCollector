

using MeterReadingCollector.Data.Entities;

namespace MeterReadingCollector.Data.Repositories;

public interface IMeterReadingRepository
{
    Task<bool> MeterReadingExistsAsync(int accountId, DateTime readingDateTime);
    Task<bool> AccountExistsAsync(int accountId);
    Task AddAsync(MeterReading meterReading);
    Task<int> SaveChangesAsync();
}
