var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRequestLocalization();
app.UseRouting();
app.MapControllers();

app.Run();