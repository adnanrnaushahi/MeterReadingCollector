using CsvHelper;
using MeterReadingCollector.Data.Context;
using MeterReadingCollector.Data.Entities;
using System.Globalization;
using MeterReadingCollector.Common;

namespace MeterReadingCollector.Api.Extensions.WebApplication;

public static class SeedDatabase
{
    public static void AddSeedData(this Microsoft.AspNetCore.Builder.WebApplication app, ApplicationSettings applicationSettings)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MeterReadingContext>();

        if (applicationSettings.EnableDataSeeding)
        {
            var csvPath = Path.Combine(AppContext.BaseDirectory, "SeedData/Test_Accounts.csv");
            if (!File.Exists(csvPath)) return;
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var accounts = csv.GetRecords<Account>().ToList();
            db.Accounts.AddRange(accounts);
            db.SaveChanges();
        }
    }
}