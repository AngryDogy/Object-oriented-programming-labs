using Banks.Entities;

namespace Banks.Tools;

public class InvalidBankOperation : Exception
{
    public InvalidBankOperation()
    {
    }

    public InvalidBankOperation(string message)
    {
    }
}