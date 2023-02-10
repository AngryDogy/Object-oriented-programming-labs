namespace Banks.Console;

public class CommandsReader
{
    private CommandParser _parser;
    public CommandsReader(CommandParser parser)
    {
        if (parser is null)
        {
            throw new NullReferenceException("Parser is null");
        }

        _parser = parser;
    }

    public void Start()
    {
        while (true)
        {
            try
            {
                string? input = System.Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    throw new InvalidOperationException("Input is null");
                Command command = _parser.Parse(input);
                command.Execute();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}