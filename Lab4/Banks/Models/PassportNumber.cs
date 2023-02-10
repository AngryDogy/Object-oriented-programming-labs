using Banks.Tools;

namespace Banks.Models;

public class PassportNumber
{
    private const long LowerBoundForNumber = 10000000000;
    public PassportNumber(long number)
    {
        if (number < LowerBoundForNumber)
        {
            throw new InvalidPassportNumberException("invalid passport number");
        }

        Number = number;
    }

    public long Number { get; }
}