using Banks.Tools;

namespace Banks.Entities;

public class DebitAccount : IAccount
{
    private const int DaysInYear = 365;
    private int _days = 0;
    public DebitAccount(Guid id, Client accountOwner, Bank accountBank)
    {
        if (accountOwner is null)
        {
            throw new NullReferenceException("AccountOwner is null");
        }

        if (accountBank is null)
        {
            throw new NullReferenceException("AccountBank is null");
        }

        Id = id;
        Money = 0;
        Type = "debit";
        AccountOwner = accountOwner;
        AccountBank = accountBank;
    }

    public Guid Id { get; }
    public decimal Money { get; private set; }
    public Client AccountOwner { get; }
    public Bank AccountBank { get; }
    public string Type { get; }
    public decimal InterestMoney { get; private set; } = 0;
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

        if (Money - moneyAmount < 0)
        {
            throw new InvalidAccountOperation("The account doesn't have enough money");
        }

        Money -= moneyAmount;
        return moneyAmount;
    }

    public void InterestPay(int time)
    {
        if (time < 0)
        {
            throw new ArgumentException("Time can't be negative");
        }

        for (int i = 0; i < time; i++)
        {
            if (_days == 30)
            {
                DepositMoney(Math.Round(InterestMoney, 2, MidpointRounding.ToEven));
                _days = 0;
                InterestMoney = 0;
            }

            InterestMoney += Money * (AccountBank.BankData.DebitInterest / DaysInYear);
            _days++;
        }
    }
}