namespace Backups.Extra.Entities;

public class Logger
{
    public Logger(bool addDateToLogs)
    {
        AddDateToLogs = addDateToLogs;
    }

    public bool AddDateToLogs { get; }
    public string Log(string message)
    {
        string date = string.Empty;
        if (AddDateToLogs)
            date = DateTime.Now.ToString();
        return $"{date} : {message}";
    }
}