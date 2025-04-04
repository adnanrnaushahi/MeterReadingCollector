using System.Globalization;
using CsvHelper;
using MeterReadingCollector.Data.Entities;

namespace MeterReadingCollector.Business.CsvHelpers;

public static class CsvHelpers
{
    public static List<Account> LoadAccountsFromCsv(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Account>().ToList();
    }

    public static List<Models.MeterReading> LoadMeterReadingFromCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Models.MeterReading>().ToList();
    }
}
