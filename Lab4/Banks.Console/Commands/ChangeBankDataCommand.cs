using Banks.Entities;

namespace Banks.Console;

public class ChangeBankDataCommand : Command
{
    public override void Execute()
    {
        int bankId = GetIntValue("Enter bank id: ");
        Bank bank = CentralBank.GetBank(IdChanger.OldId[bankId]);
        string type = GetStringValue("Enter a name of value you want to change: ");
        decimal value = GetDecimalValue("Enter new value: ");
        switch (type)
        {
            case "DebitInterest":
                CentralBank.ChangeDebitInterest(bank, value);
                break;
            case "LowDepositInterest":
                CentralBank.ChangeLowDepositInterest(bank, value);
                break;
            case "LowDepositBorder":
                CentralBank.ChangeLowDepositBorder(bank, value);
                break;
            case "MiddleDepositInterest":
                CentralBank.ChangeMiddleDepositInterest(bank, value);
                break;
            case "MiddleDepositBorder":
                CentralBank.ChangeMiddleDepositBorder(bank, value);
                break;
            case "HighDepositInterest":
                CentralBank.ChangeHighDepositInterest(bank, value);
                break;
            case "CreditCommission":
                CentralBank.ChangeCreditCommission(bank, value);
                break;
            case "CreditLimit":
                CentralBank.ChangeCreditLimit(bank, value);
                break;
            case "LimitForSuspicious":
                CentralBank.ChangeLimitForSuspicious(bank, value);
                break;
        }

        System.Console.WriteLine("The value was changed!");
    }
}