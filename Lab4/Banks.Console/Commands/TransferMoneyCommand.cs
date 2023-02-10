using Banks.Entities;

namespace Banks.Console;

public class TransferMoneyCommand : Command
{
    public override void Execute()
    {
        int accountId1 = GetIntValue("First account id: ");
        int accountId2 = GetIntValue("Second account id: ");
        IAccount account1 = CentralBank.GetAccount(IdChanger.OldId[accountId1]);
        IAccount account2 = CentralBank.GetAccount(IdChanger.OldId[accountId2]);
        decimal moneyAmount = GetDecimalValue("Amount of money to deposit: ");
        Guid id = CentralBank.CreateTransactionTransferMoney(account1, account2, moneyAmount);
        IdChanger.ChangeId(id);
        System.Console.WriteLine($"Transaction was created. Id: {IdChanger.NewId[id]}");
    }
}