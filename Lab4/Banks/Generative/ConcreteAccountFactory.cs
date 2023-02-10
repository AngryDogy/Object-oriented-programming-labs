using Banks.Entities;

namespace Banks.Builder;

public class ConcreteAccountFactory : AccountFactory
{
    public override IAccount CreateDebitAccount(Client client, Bank bank)
    {
        return new DebitAccount(Guid.NewGuid(), client, bank);
    }

    public override IAccount CreateDepositAccount(int accountTerm, Client client, Bank bank)
    {
        return new DepositAccount(Guid.NewGuid(), accountTerm, client, bank);
    }

    public override IAccount CreateCreditAccount(Client client, Bank bank)
    {
        return new CreditAccount(Guid.NewGuid(), client, bank);
    }
}