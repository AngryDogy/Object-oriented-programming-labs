using Banks.Tools;

namespace Banks.Entities;

public class CreditAccount : IAccount
{
    public CreditAccount(Guid id, Client accountOwner, Bank accountBank)
    {
        if (accountOwner is null)
        {
            throw new NullReferenceException("accountOwner is null");
        }

        if (accountBank is null)
        {
            throw new NullReferenceException("Account bank is null");
        }

        Id = id;
        Money = 0;
        Type = "credit";
        AccountOwner = accountOwner;
        AccountBank = accountBank;
    }

    public Guid Id { get; }
    public decimal Money { get; private set; }
    public Client AccountOwner { get; }
    public Bank AccountBank { get; }
    public string Type { get; }

    public void DepositMoney(decimal moneyAmount)
    {
        if (moneyAmount < 0)
        {
            throw new ArgumentException("MoneyAmount can't be negative");
        }

        Money += moneyAmount;
    }

    public decimal WithdrawMoney(decimal moneyAmount)
    {
        if (moneyAmount < 0)
        {
            throw new ArgumentException("MoneyAmount can't be negative");
        }

        if (AccountOwner.IsSuspicious)
        {
            if (moneyAmount > AccountBank.BankData.LimitForSuspicious)
            {
                throw new InvalidAccountOperation("Can't withdraw this amount of money. The client is suspicious");
            }
        }

        if (Money < 0)
        {
            moneyAmount += AccountBank.BankData.CreditCommission;
        }

        if (Money - moneyAmount < AccountBank.BankData.CreditLimit)
        {
            throw new InvalidAccountOperation("CreditLimit was exceeded");
        }

        Money -= moneyAmount;
        return moneyAmount;
    }
}