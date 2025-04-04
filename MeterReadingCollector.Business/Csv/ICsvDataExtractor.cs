namespace MeterReadingCollector.Business.Csv;

public  interface ICsvDataExtractor
{
    List<Models.MeterReading> LoadMeterReadingFromCsv(Stream stream);
}
