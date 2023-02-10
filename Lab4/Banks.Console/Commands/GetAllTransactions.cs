namespace Banks.Console;

public class GetAllTransactions : Command
{
    public override void Execute()
    {
        foreach (var transaction in CentralBank.Transactions)
        {
            System.Console.WriteLine($"Id: {IdChanger.NewId[transaction.Id]}, Money: {transaction.Money}" + $"," +
                                     $" WasCanceled: {transaction.WasCanceled})");
        }
    }
}