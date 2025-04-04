using FluentValidation.Results;
using MeterReadingCollector.Business.Models;

namespace MeterReadingCollector.Business.Validators;
public interface IMeterReadingValidator
{
    ValidationResult Validate(Data.Entities.MeterReading reading);
}
