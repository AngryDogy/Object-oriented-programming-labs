namespace Banks.Entities;

public interface IAccount
{
    Guid Id { get; }
    decimal Money { get; }
    public Client AccountOwner { get; }
    public Bank AccountBank { get; }
    public string Type { get; }
    void DepositMoney(decimal moneyAmount);
    decimal WithdrawMoney(decimal moneyAmount);
}