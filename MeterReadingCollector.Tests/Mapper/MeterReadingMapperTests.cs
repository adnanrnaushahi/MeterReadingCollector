using MeterReadingCollector.Business.Mapper;
using MeterReadingCollector.Business.Models;

namespace MeterReadingCollector.Tests.Mapper;

public class MeterReadingMapperTests
{
    [Fact]
    public void Map_WhenInputIsValid_ShouldReturnMappedMeterReading()
    {
        // Arrange
        var input = new MeterReading
        {
            AccountId = "123",
            MeterReadingDateTime = "2025-04-04 12:30:00",
            MeterReadValue = "250"
        };

        // Act
        var result = input.Map();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(123, result.AccountId);
        Assert.Equal(DateTime.Parse("2025-04-04 12:30:00"), result.MeterReadingDateTime);
        Assert.Equal(250, result.MeterReadValue);
    }
}
