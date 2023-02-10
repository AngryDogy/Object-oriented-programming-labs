namespace Banks.Console;

public class CommandParser
{
    public Command Parse(string command)
    {
        string[] words = command.Split();
        string keyWord = words[0];
        switch (keyWord)
        {
            case "/registerBank":
                return new RegisterBankCommand();
            case "/createClient":
                return new CreateClientCommand();
            case "/createAccount":
                return new CreateAccountCommand();
            case "/depositMoney":
                return new DepositMoneyCommand();
            case "/withdrawMoney":
                return new WithdrawMoneyCommand();
            case "/transferMoney":
                return new TransferMoneyCommand();
            case "/changeBankData":
                return new ChangeBankDataCommand();
            case "/cancelTransaction":
                return new CancelTransactionCommand();
            case "/increaseTime":
                return new IncreaseTimeCommand();
            case "/getAllClient":
                return new GetAllClients();
            case "/getAllBanks":
                return new GetAllBanks();
            case "/getAllTransactions":
                return new GetAllTransactions();
            case "/getAllAccounts":
                return new GetAllAccounts();
            case "/quit":
                System.Environment.Exit(0);
                return new DefaultCommand();
            default:
                return new DefaultCommand();
        }
    }
}