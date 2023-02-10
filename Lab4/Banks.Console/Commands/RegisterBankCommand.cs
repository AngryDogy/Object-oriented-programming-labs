using Banks.Entities;
using Banks.Models;
using Banks.Services;

namespace Banks.Console;

public class RegisterBankCommand : Command
{
    public override void Execute()
    {
        string name = GetStringValue("Enter a bank name: ");
        System.Console.WriteLine("Enter some bank data: ");
        decimal debitInterest = GetDecimalValue("DebitInterest: ");
        decimal lowDepositInterest = GetDecimalValue("LowDepositInterest: ");
        decimal lowDepositBorder = GetDecimalValue("LowDepositBorder: ");
        decimal middleDepositInterest = GetDecimalValue("MiddleDepositInterest: ");
        decimal middleDepositBorder = GetDecimalValue("MiddleDepositBorder: ");
        decimal highDepositInterest = GetDecimalValue("HighDepositInterest: ");
        decimal creditCommission = GetDecimalValue("CreditCommission: ");
        decimal creditLimit = GetDecimalValue("CreditLimit: ");
        decimal limitForSuspicious = GetDecimalValue("LimitForSuspicious: ");
        var bank = CentralBank.RegisterBank(
            name,
            new BankData(
                debitInterest,
                lowDepositInterest,
                lowDepositBorder,
                middleDepositInterest,
                middleDepositBorder,
                highDepositInterest,
                creditCommission,
                creditLimit,
                limitForSuspicious));
        IdChanger.ChangeId(bank.Id);
        System.Console.WriteLine($"{bank.Name} was registered. Id: {IdChanger.NewId[bank.Id]}");
    }
}