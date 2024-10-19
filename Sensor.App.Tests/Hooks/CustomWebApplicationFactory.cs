using System.Data.Common;
using Adapter.SQLite.Context;
using Core.SpiPort;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Moq;

namespace Sensor.App.Tests.Hooks;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context,services) =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<TemperatureContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);
            
            // Create open SqliteConnection so EF won't automatically close it.
           services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection(context.Configuration.GetConnectionString("IntegrationTestConnection"));
                connection.Open();

                return connection;
            });

            services.AddDbContext<TemperatureContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        });
        builder.UseEnvironment("Development");
    }
}