using Banks.Services;

namespace Banks.Console;

public class IncreaseTimeCommand : Command
{
    public override void Execute()
    {
        int days = GetIntValue("Enter time in days: ");
        CentralBankTime time = CentralBankTime.GetInstance();
        time.IncreaseTime(days);
    }
}