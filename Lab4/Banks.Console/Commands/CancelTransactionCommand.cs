namespace Banks.Console;

public class CancelTransactionCommand : Command
{
    public override void Execute()
    {
        int transactionId = GetIntValue("Transaction id: ");
        CentralBank.CancelTransaction(IdChanger.OldId[transactionId]);
    }
}