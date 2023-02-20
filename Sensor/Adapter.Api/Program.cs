using Adapter.SQLLit;
using Adapter.SQLLit.Context;
using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Adapter.TemperatureCaptor;
using Core.ApiPort;
using Core.SpiPort;
using Core.UseCase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddTransient<IRetrieveSensorState, RetrieveSensorState>();

builder.Services.AddTransient<ICaptorPort, DummyTemperatureCaptor>();

builder.Services.AddScoped<ISensorStateRepositoryPort,UnitOfWork>(); 

builder.Services.AddScoped<IRepository<SensorState>, Repository<SensorState>>();

builder.Services.AddDbContext<TemperatureContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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