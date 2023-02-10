using Banks.Interfaces;
using Banks.Models;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities;

public class Bank : ISubject
{
    private List<Client> _clients;
    private List<IAccount> _accounts;
    public Bank(Guid id, string name, BankData bankData)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new NullReferenceException("name is null");
        }

        if (bankData is null)
        {
            throw new NullReferenceException("BankData is null");
        }

        Id = id;
        Name = name;
        _clients = new List<Client>();
        _accounts = new List<IAccount>();
        BankData = bankData;
    }

    public Guid Id { get; }
    public string Name { get; }
    public BankData BankData { get; }
    public CentralBankTime Time { get; } = CentralBankTime.GetInstance();
    public IReadOnlyCollection<Client> Clients => _clients.AsReadOnly();
    public IReadOnlyCollection<IAccount> Accounts => _accounts.AsReadOnly();

    public void AddClient(Client client)
    {
        if (client is null)
        {
            throw new NullReferenceException("Client is null");
        }

        _clients.Add(client);
    }

    public void AddAccount(Client client, IAccount account)
    {
        if (client is null)
        {
            throw new NullReferenceException("Client in null");
        }

        if (account is null)
        {
            throw new NullReferenceException("Account is null");
        }

        if (!_clients.Contains(client))
        {
            throw new InvalidBankOperation("The bank doesn't contain the client");
        }

        _accounts.Add(account);
        client.AddAccount(account);
    }

    public void InterestPayment(int time)
    {
        foreach (var account in _accounts)
        {
            if (account.Type == "debit")
            {
                var debitAccount = (DebitAccount)account;
                debitAccount.InterestPay(time);
            }

            if (account.Type == "deposit")
            {
                var depositAccount = (DepositAccount)account;
                depositAccount.InterestPay(time);
            }
        }
    }

    public void Attach(IObserver observer)
    {
        var client = (Client)observer;
        if (client is null)
        {
            throw new NullReferenceException("Client is null");
        }

        client.ChangeSubscribeStatus(true);
    }

    public void Detach(IObserver observer)
    {
        var client = (Client)observer;
        if (client is null)
        {
            throw new NullReferenceException("Client is null");
        }

        client.ChangeSubscribeStatus(false);
    }

    public void Notify(string message)
    {
        var subscribedClient = _clients.Where(x => x.IsSubscribed);
        foreach (var client in subscribedClient)
        {
            client.Update(message);
        }
    }
}