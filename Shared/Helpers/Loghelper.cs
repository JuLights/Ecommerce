using ILogger = Serilog.ILogger;

namespace Shared.Helpers;

public class LogHelper(ILogger logger) : ILogHelper
{
    public void LogInfo(params string? [] args) => logger.Information("{@args}", args);
}
public interface ILogHelper
{
    void LogInfo(params string? [] args);
}