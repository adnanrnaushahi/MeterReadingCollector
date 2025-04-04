using MeterReadingCollector.Business.CsvHelpers;
using MeterReadingCollector.Data.Context;

namespace MeterReadingCollector.Api.Extensions.WebApplication;

public static class SeedDatabase
{
    public static void AddSeedData(this Microsoft.AspNetCore.Builder.WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MeterReadingContext>();

        var csvPath = Path.Combine(AppContext.BaseDirectory, "SeedData/Test_Accounts.csv");
        if (!File.Exists(csvPath)) return;
        var accounts = CsvHelpers.LoadAccountsFromCsv(csvPath);
        db.Accounts.AddRange(accounts);
        db.SaveChanges();
    }
}