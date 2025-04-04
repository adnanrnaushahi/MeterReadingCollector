using MeterReadingCollector.Business.Csv;
using MeterReadingCollector.Business.Services;
using MeterReadingCollector.Business.Validators;
using MeterReadingCollector.Data.Repositories;

namespace MeterReadingCollector.Api.Extensions;

public static class Infrastructure
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();

        services.AddTransient<ICsvDataExtractor, CsvDataExtractor>();
        services.AddTransient<IMeterReadingValidator, MeterReadingValidator>();
        services.AddTransient<IMeterReadingService, MeterReadingService>();
    }
}
