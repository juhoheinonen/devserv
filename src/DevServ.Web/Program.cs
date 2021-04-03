using System;
using Autofac.Extensions.DependencyInjection;
using DevServ.Infrastructure;
using Microsoft.AspNetCore.Hosting;
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
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            //.WriteTo.ApplicationInsights()
            .CreateLogger();

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
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

    }
}