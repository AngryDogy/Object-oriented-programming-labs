using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Services;

public class TransactionWithdrawMoney : ITransaction
{
    public TransactionWithdrawMoney(IAccount account, decimal money)
    {
        if (account is null)
        {
            throw new NullReferenceException("Account is null");
        }

        if (money < 0)
        {
            throw new ArgumentException("Money can't be negative");
        }

        account.WithdrawMoney(money);
        Account = account;
        Money = money;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public decimal Money { get; }
    public IAccount Account { get; }
    public bool WasCanceled { get; private set; }
    public void Cancel()
    {
        if (WasCanceled)
            throw new InvalidTransactionOperation("The transaction was already canceled");
        Account.DepositMoney(Money);
        WasCanceled = true;
    }
}