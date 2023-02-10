using Banks.Entities;

namespace Banks.Console;

public class CreateAccountCommand : Command
{
    public override void Execute()
    {
        int clientId = GetIntValue("Client id: ");
        Client client = CentralBank.GetClient(IdChanger.OldId[clientId]);
        System.Console.WriteLine("Choose account type: \n" +
                                 "1. Debit\n" +
                                 "2. Deposit\n" +
                                  "3. Credit");
        int digit = GetIntValue("Enter the digit: ");
        IAccount account;
        switch (digit)
        {
            case 1:
                account = CentralBank.AddDebitAccount(client);
                break;
            case 2:
                int accountTerm = GetIntValue("Enter the account term in days: ");
                account = CentralBank.AddDepositAccount(client, accountTerm);
                break;
            case 3:
                account = CentralBank.AddCreditAccount(client);
                break;
            default:
                throw new InvalidOperationException("Wrong variant choice");
        }

        IdChanger.ChangeId(account.Id);
        System.Console.WriteLine($"Account {account.Type} was created. Id: {IdChanger.NewId[account.Id]}");
    }
}