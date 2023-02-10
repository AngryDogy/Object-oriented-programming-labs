using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class Client : IObserver
{
    private List<IAccount> _accounts;
    private List<string> _updates;
    public Client(Guid id, Bank bank, ClientName clientName, Address? address, PassportNumber? passportNumber)
    {
        if (clientName is null)
        {
            throw new NullReferenceException("ClientName is null");
        }

        if (bank is null)
        {
            throw new NullReferenceException("Bank is null");
        }

        _accounts = new List<IAccount>();
        _updates = new List<string>();
        Id = id;
        ClientBank = bank;
        ClientName = clientName;
        Address = address;
        PassportNumber = passportNumber;
        if (Address != null || PassportNumber != null)
        {
            IsSuspicious = false;
        }
        else
        {
            IsSuspicious = true;
        }
    }

    public IReadOnlyCollection<IAccount> Accounts => _accounts.AsReadOnly();
    public Guid Id { get; }
    public ClientName ClientName { get; }
    public Address? Address { get; private set; }
    public PassportNumber? PassportNumber { get; private set; }
    public bool IsSuspicious { get; private set; }
    public Bank ClientBank { get; private set; }
    public bool IsSubscribed { get; private set; } = false;
    public IReadOnlyCollection<string> Updates => _updates.AsReadOnly();

    public void AddAccount(IAccount account)
    {
        if (account is null)
        {
            throw new NullReferenceException("Account is null");
        }

        _accounts.Add(account);
    }

    public void Update(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            throw new NullReferenceException("Message is null");
        }

        _updates.Add(message);
    }

    public void ChangeSubscribeStatus(bool value)
    {
        IsSubscribed = value;
    }
}