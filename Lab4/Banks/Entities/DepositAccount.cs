using Banks.Services;
using Banks.Tools;

namespace Banks.Entities;

public class DepositAccount : IAccount
{
    private const int DaysInYear = 365;
    private int _days = 0;
    public DepositAccount(Guid id, int accountTerm, Client accountOwner, Bank accountBank)
    {
        if (accountTerm < 0)
        {
            throw new ArgumentException("AccountTerm can't be negative");
        }

        if (accountOwner is null)
        {
            throw new NullReferenceException("AccountOwner is null");
        }

        if (accountBank is null)
        {
            throw new NullReferenceException("AccountBank is null");
        }

        Id = id;
        AccountTerm = accountTerm;
        Money = 0;
        Type = "deposit";
        AccountOwner = accountOwner;
        AccountBank = accountBank;
    }

    public Guid Id { get; }
    public decimal Money { get; private set; }
    public Client AccountOwner { get; }
    public Bank AccountBank { get; }
    public string Type { get; }
    public int AccountTerm { get; }
    public decimal IncomeProcent { get; private set; }
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

        if (moneyAmount > Money)
        {
            throw new InvalidAccountOperation("Can't withdraw this amount of money");
        }

        if (AccountOwner.IsSuspicious)
        {
            if (moneyAmount > AccountBank.BankData.LimitForSuspicious)
            {
                throw new InvalidAccountOperation("Can't withdraw this amount of money. The client is suspicious");
            }
        }

        if (AccountTerm >= AccountBank.Time.CurrentTime)
        {
            Money -= moneyAmount;
            return moneyAmount;
        }

        throw new InvalidAccountOperation("Can't withdraw money, term of the account is not over");
    }

    public void DetermineProcent()
    {
        if (Money < AccountBank.BankData.LowDepositBorder)
        {
            IncomeProcent = AccountBank.BankData.LowDepositInterest / DaysInYear;
            return;
        }

        if (Money >= AccountBank.BankData.LowDepositBorder && Money < AccountBank.BankData.MiddleDepositBorder)
        {
            IncomeProcent = AccountBank.BankData.MiddleDepositInterest / DaysInYear;
            return;
        }

        IncomeProcent = AccountBank.BankData.HighDepositInterest / DaysInYear;
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

            DetermineProcent();
            InterestMoney += Money * IncomeProcent;
            _days++;
        }
    }
}