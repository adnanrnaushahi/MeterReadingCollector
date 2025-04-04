using MeterReadingCollector.Business.Models;

namespace MeterReadingCollector.Business.Mapper;

public static class MeterReadingMapper
{
    public static Data.Entities.MeterReading Map(this Reading reading)
    {
        if (!DateTime.TryParse(reading.MeterReadingDateTime, out var readingDateTime))
            return null;

        return new Data.Entities.MeterReading
        {
            AccountId = int.Parse(reading.AccountId),
            MeterReadingDateTime = readingDateTime,
            MeterReadValue = int.Parse(reading.MeterReadValue)
        };
    }
}
