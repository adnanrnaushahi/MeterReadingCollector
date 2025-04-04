using MeterReadingCollector.Business.Validators;
using MeterReadingCollector.Data.Entities;

namespace MeterReadingCollector.Tests.Validator;

public class MeterReadingValidatorTests
{
    private readonly MeterReadingValidator _validator;

    public MeterReadingValidatorTests()
    {
        _validator = new MeterReadingValidator();
    }

    [Fact]
    public void Validate_ValidMeterReading_ReturnsValidResult()
    {
        // Arrange
        var meterReading = new MeterReading
        {
            AccountId = 1001,
            MeterReadingDateTime = DateTime.Now,
            MeterReadValue = 12345
        };

        // Act
        var result = _validator.Validate(meterReading);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_InvalidAccountId_ReturnsInvalidResult(int accountId)
    {
        // Arrange
        var meterReading = new MeterReading
        {
            AccountId = accountId,
            MeterReadingDateTime = DateTime.Now,
            MeterReadValue = 12345
        };

        // Act
        var result = _validator.Validate(meterReading);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == "AccountId");
        Assert.Contains(result.Errors, error => error.ErrorMessage.Contains("AccountId can't null or zero"));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(982321)]
    [InlineData(23423423)]
    public void Validate_InvalidMeterValues_ReturnsInvalidResult(int reading)
    {
        // Arrange
        var meterReading = new MeterReading
        {
            AccountId = 123,
            MeterReadingDateTime = DateTime.Now,
            MeterReadValue = reading
        };

        // Act
        var result = _validator.Validate(meterReading);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == "MeterReadValue");
        Assert.Contains(result.Errors, error => error.ErrorMessage.Contains("Meter reading must be between 0 and 99999."));
    }
}


