using FluentValidation.Results;

namespace MeterReadingCollector.Business.Validators;
public interface IMeterReadingValidator
{
    ValidationResult Validate(Data.Entities.MeterReading reading);
}
