using MeterReadingCollector.Api.Extensions;
using MeterReadingCollector.Api.Extensions.WebApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath((Path.Combine(Directory.GetCurrentDirectory(), "Configuration")));
builder.Configuration.AddJsonFile("ApplicationSettings.json");


builder.Services.AddServiceMvc();
builder.Services.AddSwagger();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.AddSeedData();
app.UseRequestLocalization();
app.UseRouting();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();