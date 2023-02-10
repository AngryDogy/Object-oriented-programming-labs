using Banks.Services;

namespace Banks.Console;

public abstract class Command
{
    public CentralBank CentralBank { get;  } = CentralBank.GetInstance();
    public abstract void Execute();

    public string GetStringValue(string message)
    {
        System.Console.Write(message);
        return System.Console.ReadLine() ?? string.Empty;
    }

    public decimal GetDecimalValue(string message)
    {
        System.Console.Write(message);
        return decimal.Parse(System.Console.ReadLine() ?? string.Empty);
    }

    public int GetIntValue(string message)
    {
        System.Console.Write(message);
        return int.Parse(System.Console.ReadLine() ?? string.Empty);
    }

    public Guid GetGuidValue(string message)
    {
        System.Console.Write(message);
        return Guid.Parse(System.Console.ReadLine() ?? string.Empty);
    }

    public long GetLongValue(string message)
    {
        System.Console.Write(message);
        return long.Parse(System.Console.ReadLine() ?? string.Empty);
    }

    public bool MakeDecision(string message)
    {
        System.Console.Write(message + "(y/n)");
        string ans = System.Console.ReadLine() ?? string.Empty;
        if (ans == "y")
            return true;
        if (ans == "n")
            return false;
        throw new InvalidOperationException("Invalid choice");
    }
}