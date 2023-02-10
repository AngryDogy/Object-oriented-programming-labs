using Banks.Entities;

namespace Banks.Console;

public class GetAllAccounts : Command
{
    public override void Execute()
    {
        foreach (Bank bank in CentralBank.Banks)
        {
            foreach (IAccount account in bank.Accounts)
            {
                System.Console.WriteLine($"Type: {account.Type}, Money: {account.Money}, Id: {IdChanger.NewId[account.Id]}");
            }
        }
    }
}