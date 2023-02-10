using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console;

public class DepositMoneyCommand : Command
{
    public override void Execute()
    {
        int accountId = GetIntValue("Account id: ");
        IAccount account = CentralBank.GetAccount(IdChanger.OldId[accountId]);
        decimal moneyAmount = GetDecimalValue("Amount of money to deposit: ");
        Guid id = CentralBank.CreateTransactionDepositMoney(account, moneyAmount);
        IdChanger.ChangeId(id);
        System.Console.WriteLine($"Transaction was created. Id: {IdChanger.NewId[id]}");
    }
}