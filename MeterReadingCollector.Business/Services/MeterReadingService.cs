﻿using MeterReadingCollector.Business.Csv;
using MeterReadingCollector.Business.Mapper;
using MeterReadingCollector.Business.Models;
using MeterReadingCollector.Business.Validators;
using MeterReadingCollector.Data.Repositories;
using Microsoft.AspNetCore.Http;

namespace MeterReadingCollector.Business.Services;

public class MeterReadingService(IMeterReadingValidator validator, IMeterReadingRepository repository, ICsvDataExtractor csvDataExtractor)
    : IMeterReadingService
{

    public async Task<MeterReadingResponse> ProcessCsvFileAsync(IFormFile file)
    {
        var response = new MeterReadingResponse();

        var meterReadings = await LoadMeterReadingsFromCvs(file);

        foreach (var reading in meterReadings)
        {
            var dto = reading.Map();
            var result = validator.Validate(dto);
            if (!result.IsValid)
            {
                response.Failed++;
                continue;
            }

            if (!await repository.AccountExistsAsync(dto.AccountId))
            {
                response.Failed++;
                continue;
            }

            if (await repository.MeterReadingExistsAsync(dto.AccountId, dto.MeterReadingDateTime))
            {
                response.Failed++;
                continue;
            }

            await repository.AddAsync(dto);
            response.Successful++;

        }

        await repository.SaveChangesAsync();
        return response;
    }

    private async Task<List<MeterReading>> LoadMeterReadingsFromCvs(IFormFile file)
    {
        List<MeterReading> meterReadings;
        await using var stream = file.OpenReadStream();
        try
        {
            meterReadings = csvDataExtractor.LoadMeterReadingFromCsv(stream);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error parsing CSV: {ex.Message}");
        }

        return meterReadings;
    }
}