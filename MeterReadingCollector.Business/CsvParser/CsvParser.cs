using System.Globalization;
using CsvHelper;

namespace MeterReadingCollector.Business.CsvParser;

public class CsvParser : ICsvParser
{
    public List<Models.MeterReading> LoadMeterReadingFromCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Models.MeterReading>().ToList();
    }
}
