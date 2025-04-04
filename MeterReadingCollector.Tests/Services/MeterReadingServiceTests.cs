using FluentValidation.Results;
using MeterReadingCollector.Business.CsvParser;
using MeterReadingCollector.Business.Models;
using MeterReadingCollector.Business.Services;
using MeterReadingCollector.Business.Validators;
using MeterReadingCollector.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;

namespace MeterReadingCollector.Tests.Services;

public class MeterReadingServiceTests
{
    private readonly Mock<IMeterReadingValidator> _mockValidator;
    private readonly Mock<IMeterReadingRepository> _mockRepository;
    private readonly Mock<ICsvDataExtractor> _mockCsvParser;
    private readonly Mock<IFormFile> _mockFile;
    private readonly MeterReadingService _service;

    public MeterReadingServiceTests()
    {
        _mockValidator = new Mock<IMeterReadingValidator>();
        _mockRepository = new Mock<IMeterReadingRepository>();
        _mockCsvParser = new Mock<ICsvDataExtractor>();
        _mockFile = new Mock<IFormFile>();

        _mockFile.Setup(file => file.OpenReadStream()).Returns(new MemoryStream());
        _mockValidator.Setup(v => v.Validate(It.IsAny<Data.Entities.MeterReading>())).Returns(new ValidationResult());
        _service = new MeterReadingService(_mockValidator.Object, _mockRepository.Object, _mockCsvParser.Object);
    }

    [Fact]
    public async Task ProcessCsvFileAsync_WhenValidReadings_ShouldReturnSuccessfulResponse()
    {
        //Arrange

        var meterReadings = new List<MeterReading>
            {
                new()  { AccountId = "123", MeterReadingDateTime = DateTime.Now.ToString("O"), MeterReadValue = "2342" },
                new() { AccountId = "124", MeterReadingDateTime = DateTime.Now.AddDays(-1).ToString("O"), MeterReadValue = "23421" }
            };

        _mockCsvParser.Setup(p => p.LoadMeterReadingFromCsv(It.IsAny<Stream>())).Returns(meterReadings);
        _mockRepository.Setup(r => r.AccountExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _mockRepository.Setup(r => r.MeterReadingExistsAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(false);
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Data.Entities.MeterReading>())).Returns(Task.CompletedTask);

        //Act
        var result = await _service.ProcessCsvFileAsync(new Mock<IFormFile>().Object);

        // Assert
        Assert.Equal(2, result.Successful);
        Assert.Equal(0, result.Failed);
    }

    [Fact]
    public async Task ProcessCsvFileAsync_WhenValidationFails_ShouldIncrementFailed()
    {
        // Arrange
        var meterReadings = new List<MeterReading>
        {
            new() { AccountId = "123", MeterReadingDateTime = DateTime.Now.ToString("O"), MeterReadValue = "67542342" },
        };

        _mockValidator.Setup(v => v.Validate(It.IsAny<Data.Entities.MeterReading>())).Returns(new ValidationResult { 
            Errors = 
            { new ValidationFailure("MeterReadValue", "MeterReadValue must be between 0 and 99999.") }
           
        });
        _mockCsvParser.Setup(p => p.LoadMeterReadingFromCsv(It.IsAny<Stream>())).Returns(meterReadings);
        _mockRepository.Setup(r => r.AccountExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _mockRepository.Setup(r => r.MeterReadingExistsAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(false);

        // Act
        var result = await _service.ProcessCsvFileAsync(_mockFile.Object);

        // Assert
        Assert.Equal(0, result.Successful);
        Assert.Equal(1, result.Failed);
    }

    [Fact]
    public async Task ProcessCsvFileAsync_WhenAccountDoesNotExist_ShouldIncrementFailed()
    {
        // Arrange
        var meterReadings = new List<MeterReading>
        {
            new() { AccountId = "123", MeterReadingDateTime = DateTime.Now.ToString("O"), MeterReadValue = "6754" },
        };

        _mockCsvParser.Setup(p => p.LoadMeterReadingFromCsv(It.IsAny<Stream>())).Returns(meterReadings);
        _mockRepository.Setup(r => r.AccountExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
        _mockRepository.Setup(r => r.MeterReadingExistsAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(false);

        // Act
        var result = await _service.ProcessCsvFileAsync(_mockFile.Object);

        // Assert
        Assert.Equal(0, result.Successful);
        Assert.Equal(1, result.Failed);
    }

    [Fact]
    public async Task ProcessCsvFileAsync_WhenMeterReadingAlreadyExists_ShouldIncrementFailed()
    {
        // Arrange
        var meterReadings = new List<MeterReading>
            {
                new() { AccountId = "123", MeterReadingDateTime = DateTime.Now.ToString("O"), MeterReadValue = "6754" },
            };

        _mockCsvParser.Setup(p => p.LoadMeterReadingFromCsv(It.IsAny<Stream>())).Returns(meterReadings);
        _mockRepository.Setup(r => r.AccountExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _mockRepository.Setup(r => r.MeterReadingExistsAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(true);

        // Act
        var result = await _service.ProcessCsvFileAsync(_mockFile.Object);

        // Assert
        Assert.Equal(0, result.Successful);
        Assert.Equal(1, result.Failed);
    }
}