using MeterReadingCollector.Business.CsvParser;
using MeterReadingCollector.Business.Services;
using MeterReadingCollector.Business.Validators;
using MeterReadingCollector.Data.Repositories;

namespace MeterReadingCollector.Api.Extensions;

public static class Infrastructure
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();

        services.AddTransient<ICsvParser, CsvParser>();
        services.AddTransient<IMeterReadingValidator, MeterReadingValidator>();
        services.AddTransient<IMeterReadingService, MeterReadingService>();
    }
}
