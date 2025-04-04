using MeterReadingCollector.Business.Models;

namespace MeterReadingCollector.Business.Mapper;

public static class MeterReadingMapper
{
    public static Data.Entities.MeterReading Map(this MeterReading reading)
    {
        return new Data.Entities.MeterReading
        {
            AccountId = int.Parse(reading.AccountId),
            MeterReadingDateTime = DateTime.Parse(reading.MeterReadingDateTime),
            MeterReadValue = int.Parse(reading.MeterReadValue)
        };
    }
}
