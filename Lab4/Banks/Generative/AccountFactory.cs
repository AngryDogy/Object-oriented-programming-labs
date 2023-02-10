using Banks.Entities;

namespace Banks.Builder;

public abstract class AccountFactory
{
    public abstract IAccount CreateDebitAccount(Client client, Bank bank);
    public abstract IAccount CreateDepositAccount(int accountTerm, Client client, Bank bank);
    public abstract IAccount CreateCreditAccount(Client client, Bank bank);
}