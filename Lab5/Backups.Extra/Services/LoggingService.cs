using Backups.Extra.Entities;

namespace Backups.Extra.Services;

public abstract class LoggingService
{
    public LoggingService(Logger logger)
    {
        if (logger is null)
        {
            throw new NullReferenceException("Logger is null");
        }

        Logger = logger;
    }

    public Logger Logger { get; }
    public abstract void Log(string message);
}