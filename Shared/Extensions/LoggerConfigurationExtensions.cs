using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Shared.Extensions;

public static class LoggerConfigurationExtensions
{
    public static void AddSerilogLogging(this IServiceCollection services, string projectName)
    {
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .Enrich.FromLogContext();

        Log.Logger = loggerConfig
            .WriteTo.Console()
            .CreateLogger();
        
        services.AddSingleton<ILogger>(_ => Log.Logger); 
        services.AddSingleton<ILoggerFactory>(_ =>
        {
            var factory = new SerilogLoggerFactory(Log.Logger);
            return factory;
        });

        services.AddLogging(loggingBuilder => { loggingBuilder.AddSerilog(dispose: true); });
    }
}