using System.Text.Json.Serialization;
using Adapter.Api.Providers;
using Adapter.SQLite.Context;
using Adapter.SQLite.Models;
using Adapter.SQLite.Repository;
using Adapter.TemperatureCaptor;
using Core.ApiPort;
using Core.SpiPort;
using Core.UseCase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => 
    { 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
    });
builder.Services.AddSingleton<ISensorIntervalProvider,AppSettingsSensorIntervalProvider>();
builder.Services.AddScoped<IRetrieveSensor, RetrieveSensor>();
builder.Services.AddScoped<IRetrieveLatestSensorRequests, RetrieveLatestSensorRequests>();
builder.Services.AddTransient<ITemperatureSensorPort, DummyTemperatureSensor>();
builder.Services.AddScoped<ISensorRepositoryPort,SensorRepository>();
builder.Services.AddScoped<IRepository<Sensor>, Repository<Sensor>>();

builder.Services.AddDbContext<TemperatureContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler("/Error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();


app.Run();

public partial class Program {}