using Adapter.SQLLit;
using Adapter.SQLLit.Context;
using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Adapter.TemperatureCaptor;
using Core.ApiPort;
using Core.SpiPort;
using Core.UseCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<List<ISensorInterval>>(provider =>
{
    return builder.Configuration
        .GetSection("SensorIntervals")
        .Get<List<Adapter.Api.SensorInterval>>()
        .Select(si => Adapter.Api.SensorInterval.ToSensorInterval(si)).ToList();
});

builder.Services.AddTransient<IRetrieveSensor, RetrieveSensor>();
builder.Services.AddTransient<IRetrieveLatestSensorRequests, RetrieveLatestSensorRequests>();
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
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();


app.Run();

public partial class Program {}