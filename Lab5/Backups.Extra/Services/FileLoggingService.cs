using Backups.Extra.Entities;

namespace Backups.Extra.Services;

public class FileLoggingService : LoggingService
{
    private string _path;
    public FileLoggingService(Logger logger, string logFileName)
        : base(logger)
    {
        if (string.IsNullOrEmpty(logFileName))
        {
            throw new NullReferenceException("LogFileName is null");
        }

        _path = logFileName;
    }

    public override void Log(string message)
    {
        using (var writer = new StreamWriter(_path, append: true))
        {
            writer.WriteLineAsync(Logger.Log(message));
        }
    }
}