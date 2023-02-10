namespace Banks.Console;

internal class Program
{
    public static void Main(string[] args)
    {
        var commandsReader = new CommandsReader(new CommandParser());
        commandsReader.Start();
    }
}