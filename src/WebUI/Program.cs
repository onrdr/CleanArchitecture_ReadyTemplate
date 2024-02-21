using Infrastructure.ExtensionMethods;
using Serilog;
using WebUI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Configuration.AddAppSettings();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.UseSerilog((_, config) => config
    .ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

await app.EnsureDatabaseCreated();

app.Run();
