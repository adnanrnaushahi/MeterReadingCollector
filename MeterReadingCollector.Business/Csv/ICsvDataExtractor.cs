namespace MeterReadingCollector.Business.Csv;

public  interface ICsvDataExtractor
{
    List<Models.Reading> LoadMeterReadingFromCsv(Stream stream);
}
