
using MilkRun.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MilkRun.Api.Configuration;
using Microsoft.AspNetCore.Authentication;
using MilkRun.Infrastructure.Middleware;
using MilkRun.Infrastructure;

namespace MilkRun.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                Log.Information("Creation Application Configuration");
                // Add services to the container.
                builder.Configuration.AddConfiguration(HelperConfigurationBuilder.ConfigureConfiguration());
                Log.Logger = HelperConfigurationBuilder.CreateLoggerConfiguration(builder.Configuration).CreateLogger();
                builder.Host.UseSerilog(Log.Logger);
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddHealthChecks();
                builder.Services.AddInfrastrcutureServices();
                builder.Services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthMiddlewareHandler>("BasicAuthentication", null);
                builder.Services.AddDbContext<MilkRunDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetValue<string>("MilkRunDbConnection"), sqlServerOptionsAction: sqlOptions =>
                     sqlOptions
                            .UseNetTopologySuite()
                            .EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(60),
                                errorNumbersToAdd: null));
                });

                var app = builder.Build();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseSerilogRequestLogging();
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseRouting();
                app.UseAuthorization();
                app.UseHealthChecks("/healthz");
                app.MapControllers();
                Log.Information("Application Starting");
                app.Run();               
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }


           
        }
    }
}