using System.Globalization;
using CsvHelper;
using MeterReadingCollector.Business.Models;

namespace MeterReadingCollector.Business.Csv;

public class CsvDataExtractor : ICsvDataExtractor
{
    public List<Reading> LoadMeterReadingFromCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Reading>().ToList();
    }
}
