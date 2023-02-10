using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Services;

public class TransactionTransferMoney : ITransaction
{
    public TransactionTransferMoney(IAccount account, IAccount toAccount, decimal money)
    {
        if (account is null)
        {
            throw new NullReferenceException("Account is null");
        }

        if (toAccount is null)
        {
            throw new NullReferenceException("ToAccount is null");
        }

        if (money < 0)
        {
            throw new ArgumentException("Money can't be negative");
        }

        account.WithdrawMoney(money);
        toAccount.DepositMoney(money);
        Account = account;
        ToAccount = toAccount;
        Money = money;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public decimal Money { get; }
    public IAccount Account { get; }
    public IAccount ToAccount { get; }
    public bool WasCanceled { get; private set; }
    public void Cancel()
    {
        if (WasCanceled)
            throw new InvalidTransactionOperation("The transaction was already canceled");
        ToAccount.WithdrawMoney(Money);
        Account.DepositMoney(Money);
        WasCanceled = true;
    }
}