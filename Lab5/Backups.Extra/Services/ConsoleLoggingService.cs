using Backups.Extra.Entities;

namespace Backups.Extra.Services;

public class ConsoleLoggingService : LoggingService
{
    public ConsoleLoggingService(Logger logger)
        : base(logger)
    {
    }

    public override void Log(string message)
    {
        Console.WriteLine(Logger.Log(message));
    }
}