using Banks.Entities;

namespace Banks.Console;

public class GetAllBanks : Command
{
    public override void Execute()
    {
        foreach (Bank bank in CentralBank.Banks)
        {
            System.Console.WriteLine($"Name {bank.Name}, Id: {IdChanger.NewId[bank.Id]}");
        }
    }
}