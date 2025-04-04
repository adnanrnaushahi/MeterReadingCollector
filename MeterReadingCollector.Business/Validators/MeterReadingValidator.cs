using FluentValidation;

namespace MeterReadingCollector.Business.Validators;

public class MeterReadingValidator : AbstractValidator<Data.Entities.MeterReading>, IMeterReadingValidator
{
    public MeterReadingValidator()
    {
        RuleFor(x => x.AccountId)
            .NotNull().GreaterThan(0).WithMessage("AccountId can't null or zero");

        RuleFor(x => x.MeterReadingDateTime)
            .NotEmpty().WithMessage("MeterReadingDateTime is required.");

        RuleFor(x => x.MeterReadValue)
            .Must(ValidReading).WithMessage("Meter reading must be between 0 and 99999.");

    }
    private bool ValidReading(int readingValue)
    {
        return readingValue is >= 0 and <= 99999;
    }
}
