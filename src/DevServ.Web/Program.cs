using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using DevServ.Infrastructure;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DevServ.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
                        
            var loggerConfiguration = new LoggerConfiguration().ReadFrom.Configuration(configuration);
            
            var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
            telemetryConfiguration.InstrumentationKey = configuration["ApplicationInsightsInstrumentationKey"];
            loggerConfiguration.WriteTo
                .ApplicationInsights(telemetryConfiguration,
                    TelemetryConverter.Traces);            

            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Information("Starting up");

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var context = services.GetRequiredService<DevServDbContext>();
                    context.Database.EnsureCreated();
                    SeedData.Initialize(services);
                }

                host.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An error occurred during startup.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });        
    }
}