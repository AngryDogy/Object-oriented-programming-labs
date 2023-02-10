using Banks.Builder;
using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Xunit;
using Xunit.Abstractions;
namespace Banks.Test;
public class BanksTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BanksTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CreateAccount()
    {
        var centralBank = CentralBank.GetInstance();
        var bankData = new BankData(
            5,
            7,
            50000,
            8,
            100000,
            10,
            3,
            100000,
            10000);
        var bank = centralBank.RegisterBank("Sberbank", bankData);
        var client = centralBank.AddClient(bank, "Vlad", "Hober", 68872345121);
        var account = centralBank.AddDebitAccount(client);
        account.DepositMoney(600);
        Assert.True(account.Money == 600);
    }

    [Fact]
    public void UseCentralBankTime()
    {
        var centralBank = CentralBank.GetInstance();
        var bankData = new BankData(
            5,
            7,
            50000,
            8,
            100000,
            10,
            3,
            100000,
            10000);
        var bank = centralBank.RegisterBank("Sberbank", bankData);
        var client = centralBank.AddClient(bank, "Vlad", "Hober", 68872345121);
        var account1 = centralBank.AddDebitAccount(client);
        var account2 = centralBank.AddDepositAccount(client, 50);
        var time = CentralBankTime.GetInstance();
        account1.DepositMoney(100);
        account2.DepositMoney(100);
        time.IncreaseTime(31);
        Assert.True(account1.Money == 141.10M && account2.Money == 157.53M);
    }

    [Fact]
    public void UseNotify()
    {
        var centralBank = CentralBank.GetInstance();
        var bankData = new BankData(
            5,
            7,
            50000,
            8,
            100000,
            10,
            3,
            100000,
            10000);
        var bank = centralBank.RegisterBank("Sberbank", bankData);
        var client = centralBank.AddClient(bank, "Vlad", "Hober", 68872345121);
        var account = centralBank.AddDebitAccount(client);
        bank.Attach(client);
        centralBank.ChangeDebitInterest(bank, 10);
        centralBank.ChangeCreditLimit(bank, 500000);
        Assert.True(client.Updates.Count == 2);
    }

    [Fact]
    public void UseTransactions()
    {
        var centralBank = CentralBank.GetInstance();
        var bankData = new BankData(
            5,
            7,
            50000,
            8,
            100000,
            10,
            3,
            100000,
            10000);
        var bank = centralBank.RegisterBank("Sberbank", bankData);
        var client = centralBank.AddClient(bank, "Vlad", "Hober", 68872345121);
        var account1 = centralBank.AddDebitAccount(client);
        var account2 = centralBank.AddDepositAccount(client, 100);
        centralBank.CreateTransactionDepositMoney(account1, 100);
        centralBank.CreateTransactionDepositMoney(account2, 100);
        Guid id = centralBank.CreateTransactionTransferMoney(account1, account2, 100);
        centralBank.CancelTransaction(id);
        Assert.True(account2.Money == 100);
    }
}