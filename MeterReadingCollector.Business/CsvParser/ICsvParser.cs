namespace MeterReadingCollector.Business.CsvParser;

public  interface ICsvParser
{
    List<Models.MeterReading> LoadMeterReadingFromCsv(Stream stream);
}
