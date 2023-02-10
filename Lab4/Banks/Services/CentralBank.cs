using Banks.Builder;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools;

namespace Banks.Services;

public class CentralBank
{
    private static CentralBank? _instance;
    private List<Bank> _banks;
    private List<ITransaction> _transactions;
    private CentralBankTime _time = CentralBankTime.GetInstance();
    private AccountFactory _accountFactory;
    private ClientBuilderDirector _clientBuilderDirector;
    private CentralBank(AccountFactory accountFactory, IClientBuilder clientBuilder)
    {
        if (accountFactory is null)
        {
            throw new NullReferenceException("AccountFactory is null");
        }

        if (clientBuilder is null)
        {
            throw new NullReferenceException("ClientBuilder is null");
        }

        _banks = new List<Bank>();
        _transactions = new List<ITransaction>();
        _accountFactory = accountFactory;
        _clientBuilderDirector = new ClientBuilderDirector(clientBuilder);
        _time.AddCentralBank(this);
    }

    public IReadOnlyCollection<Bank> Banks => _banks.AsReadOnly();
    public IReadOnlyCollection<ITransaction> Transactions => _transactions.AsReadOnly();
    public static CentralBank GetInstance()
    {
        if (_instance is null)
        {
            _instance = new CentralBank(new ConcreteAccountFactory(), new ClientBuilder());
        }

        return _instance;
    }

    public Bank RegisterBank(string name, BankData bankData)
    {
        var bank = new Bank(Guid.NewGuid(), name, bankData);
        _banks.Add(bank);
        return bank;
    }

    public Client AddClient(Bank bank, string name, string surname)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank is null");
        }

        _clientBuilderDirector.BuildClient(bank, name, surname);
        Client client = _clientBuilderDirector.ClientBuilder.GetClient();
        bank.AddClient(client);
        return client;
    }

    public Client AddClient(Bank bank, string name, string surname, string country, string city, string street, int houseNumber)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank is null");
        }

        _clientBuilderDirector.BuildClientWithAddress(bank, name, surname, country, city, street, houseNumber);
        Client client = _clientBuilderDirector.ClientBuilder.GetClient();
        bank.AddClient(client);
        return client;
    }

    public Client AddClient(Bank bank, string name, string surname, long passportNumber)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank is null");
        }

        _clientBuilderDirector.BuildClientWithPassportNumber(bank, name, surname, passportNumber);
        Client client = _clientBuilderDirector.ClientBuilder.GetClient();
        bank.AddClient(client);
        return client;
    }

    public Client AddClient(Bank bank, string name, string surname, string country, string city, string street, int houseNumber, long passportNumber)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank is null");
        }

        _clientBuilderDirector.BuildFullClient(bank, name, surname, country, city, street, houseNumber, passportNumber);
        Client client = _clientBuilderDirector.ClientBuilder.GetClient();
        bank.AddClient(client);
        return client;
    }

    public IAccount AddDebitAccount(Client client)
    {
        if (client is null)
        {
            throw new NullReferenceException("Client is null");
        }

        IAccount account = _accountFactory.CreateDebitAccount(client, client.ClientBank);
        client.ClientBank.AddAccount(client, account);
        return account;
    }

    public IAccount AddDepositAccount(Client client, int accountTerm)
    {
        if (client is null)
        {
            throw new NullReferenceException("Client is null");
        }

        IAccount account = _accountFactory.CreateDepositAccount(accountTerm, client, client.ClientBank);
        client.ClientBank.AddAccount(client, account);
        return account;
    }

    public IAccount AddCreditAccount(Client client)
    {
        if (client is null)
        {
            throw new NullReferenceException("Client is null");
        }

        IAccount account = _accountFactory.CreateCreditAccount(client, client.ClientBank);
        client.ClientBank.AddAccount(client, account);
        return account;
    }

    public void InterestPayment(int time)
    {
        if (time < 0)
        {
            throw new ArgumentException("Time is invalid");
        }

        foreach (Bank bank in _banks)
        {
            bank.InterestPayment(time);
        }
    }

    public void ChangeDebitInterest(Bank bank, decimal debitInterest)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(debitInterest);
        bank.Notify($"DebitInterest was changed. New value: {debitInterest}");
    }

    public void ChangeLowDepositInterest(Bank bank, decimal lowDepositInterest)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(lowDepositInterest);
        bank.Notify($"LowDepositInterest was changed. New value: {lowDepositInterest}");
    }

    public void ChangeMiddleDepositInterest(Bank bank, decimal middleDepositInterest)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(middleDepositInterest);
        bank.Notify($"MiddleDepositInterest was changed. New value: {middleDepositInterest}");
    }

    public void ChangeHighDepositInterest(Bank bank, decimal highDepositInterest)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(highDepositInterest);
        bank.Notify($"HighDepositInterest was changed. New value: {highDepositInterest}");
    }

    public void ChangeCreditCommission(Bank bank, decimal creditCommission)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(creditCommission);
        bank.Notify($"CreditCommission was changed. New value: {creditCommission}");
    }

    public void ChangeCreditLimit(Bank bank, decimal creditLimit)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(creditLimit);
        bank.Notify($"CreditLimit was changed. New value: {creditLimit}");
    }

    public void ChangeLowDepositBorder(Bank bank, decimal lowDepositBorder)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(lowDepositBorder);
        bank.Notify($"LowDepositBorder was changed. New value: {lowDepositBorder}");
    }

    public void ChangeMiddleDepositBorder(Bank bank, decimal middleDepositBorder)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeMiddleDepositBorder(middleDepositBorder);
        bank.Notify($"MiddleDepositBorder was changed. New value: {middleDepositBorder}");
    }

    public void ChangeLimitForSuspicious(Bank bank, decimal limitForSuspicious)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank in null");
        }

        bank.BankData.ChangeLimitForSuspicious(limitForSuspicious);
        bank.Notify($"LimitForSuspicious was changed. New value: {limitForSuspicious}");
    }

    public Guid CreateTransactionDepositMoney(IAccount account, decimal money)
    {
        var transaction = new TransactionDepositMoney(account, money);
        _transactions.Add(transaction);
        return transaction.Id;
    }

    public Guid CreateTransactionWithdrawMoney(IAccount account, decimal money)
    {
        var transaction = new TransactionWithdrawMoney(account, money);
        _transactions.Add(transaction);
        return transaction.Id;
    }

    public Guid CreateTransactionTransferMoney(IAccount account, IAccount toAccount, decimal money)
    {
        var transaction = new TransactionTransferMoney(account, toAccount, money);
        _transactions.Add(transaction);
        return transaction.Id;
    }

    public void CancelTransaction(Guid id)
    {
        var transaction = _transactions.SingleOrDefault(x => x.Id == id);
        if (transaction is null)
            throw new InvalidBankOperation("Can't find the transaction");
        transaction.Cancel();
    }

    public Bank GetBank(Guid id)
    {
        var bank = _banks.SingleOrDefault(x => x.Id == id);
        if (bank is null)
            throw new InvalidBankOperation("The bank doens't exist");
        return bank;
    }

    public Client GetClient(Guid id)
    {
        foreach (Bank bank in _banks)
        {
            Client? client = bank.Clients.SingleOrDefault(x => x.Id == id);
            if (client != null)
            {
                return client;
            }
        }

        throw new InvalidBankOperation("The client doesn't exist");
    }

    public IAccount GetAccount(Guid id)
    {
        foreach (Bank bank in _banks)
        {
            IAccount? account = bank.Accounts.SingleOrDefault(x => x.Id == id);
            if (account != null)
            {
                return account;
            }
        }

        throw new InvalidBankOperation("The account doens't exist");
    }
}