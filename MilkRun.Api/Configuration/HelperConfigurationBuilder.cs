using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog.Events;
using Serilog;
using Serilog.Exceptions;

namespace MilkRun.Api.Configuration
{
    public static class HelperConfigurationBuilder
    {
        public static IConfigurationRoot ConfigureConfiguration()
        {
            var applicationConfiguration = new ConfigurationBuilder();
            // Use appsettings.json as the local development JSON file,
            // and replace all params with env variables on deployment
            IConfigurationBuilder loggerBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            applicationConfiguration.AddConfiguration(loggerBuilder.Build());
            // add azure app configuration
            return applicationConfiguration.Build();
        }

        public static LoggerConfiguration CreateLoggerConfiguration(IConfiguration configuration)
        {
            string SerilogConsoleTemplate = $"[{{Timestamp:HH:mm:ss}} {{Level:u3}} {{Message:lj}}{{NewLine}}{{Exception}}";

            // logging middleware
            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.WithProperty("Environment", configuration["Environment"])
                .Enrich.WithProperty("Application", "MilkRunApi")
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails() // NOTE: adds a few extra fields into ElasticSearch              
                .WriteTo.Console(outputTemplate: SerilogConsoleTemplate)
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .MinimumLevel.Override("HealthChecks", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
                .ReadFrom.Configuration(configuration);         
            return loggerConfiguration;
        }
    }
}
