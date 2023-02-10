namespace Banks.Models;

public class BankData
{
    public BankData(
        decimal debitInterest,
        decimal lowDepositInterest,
        decimal lowDepositBorder,
        decimal middleDepositInterest,
        decimal middleDepositBorder,
        decimal highDepositInterest,
        decimal creditCommission,
        decimal creditLimit,
        decimal limitForSuspicious)
    {
        if (!CheckProcentData(debitInterest))
            throw new ArgumentException("DebitInterest is invalid");
        if (!CheckProcentData(lowDepositInterest))
            throw new ArgumentException("LowDepositInterest is invalid");
        if (!CheckValueData(lowDepositBorder))
            throw new ArgumentException("LowDepositBorder is invalid");
        if (!CheckProcentData(middleDepositInterest))
            throw new ArgumentException("MiddleDepositInterest is invalid");
        if (!CheckValueData(middleDepositBorder))
            throw new ArgumentException("MiddleDepositBorder is invalid");
        if (!CheckProcentData(highDepositInterest))
            throw new ArgumentException("HighDepositInterest is invalid");
        if (!CheckValueData(creditCommission))
            throw new ArgumentException("CreditCommission t is invalid");
        if (!CheckValueData(creditLimit))
            throw new ArgumentException("CreditLimit is invalid");
        if (!CheckValueData(limitForSuspicious))
            throw new ArgumentException("LimitForSuspicious is invalid");
        DebitInterest = debitInterest;
        LowDepositInterest = lowDepositInterest;
        LowDepositBorder = lowDepositBorder;
        MiddleDepositInterest = middleDepositInterest;
        MiddleDepositBorder = middleDepositBorder;
        HighDepositInterest = highDepositInterest;
        CreditCommission = creditCommission;
        CreditLimit = creditLimit;
        LimitForSuspicious = limitForSuspicious;
    }

    public decimal DebitInterest { get; private set; }
    public decimal LowDepositInterest { get; private set; }
    public decimal LowDepositBorder { get; private set; }
    public decimal MiddleDepositInterest { get; private set; }
    public decimal MiddleDepositBorder { get; private set; }
    public decimal HighDepositInterest { get; private set; }
    public decimal CreditCommission { get; private set; }
    public decimal CreditLimit { get; private set; }
    public decimal LimitForSuspicious { get; private set; }

    public bool CheckProcentData(decimal procent)
    {
        if (procent < 0 || procent > 100)
            return false;
        return true;
    }

    public bool CheckValueData(decimal value)
    {
        if (value < 0)
            return false;
        return true;
    }

    public void ChangeDebitInterest(decimal debitInterest)
    {
        if (!CheckProcentData(debitInterest))
            throw new ArgumentException("debitInterest is invalid");
        DebitInterest = debitInterest;
    }

    public void ChangeLowDepositInterest(decimal lowDepositInterest)
    {
        if (!CheckProcentData(lowDepositInterest))
            throw new ArgumentException("LowDepositInterest is invalid");
        LowDepositInterest = lowDepositInterest;
    }

    public void ChangeMiddleDepositInterest(decimal middleDepositInterest)
    {
        if (!CheckProcentData(middleDepositInterest))
            throw new ArgumentException("MiddleDepositInterest is invalid");
        MiddleDepositInterest = middleDepositInterest;
    }

    public void ChangeHighDepositInterest(decimal highDepositInterest)
    {
        if (!CheckProcentData(highDepositInterest))
            throw new ArgumentException("HighDepositInterest is invalid");
        HighDepositInterest = highDepositInterest;
    }

    public void ChangeCreditCommission(decimal creditCommission)
    {
        if (!CheckValueData(creditCommission))
            throw new ArgumentException("CreditCommission is invalid");
        CreditCommission = creditCommission;
    }

    public void ChangeCreditLimit(decimal creditLimit)
    {
        if (!CheckValueData(CreditLimit))
            throw new ArgumentException("CreditLimit is invalid");
        CreditLimit = CreditLimit;
    }

    public void ChangeLowDepositBorder(decimal lowDepositBorder)
    {
        if (!CheckValueData(lowDepositBorder))
            throw new ArgumentException("LowDepositBorder is invalid");
        LowDepositBorder = lowDepositBorder;
    }

    public void ChangeMiddleDepositBorder(decimal middleDepositBorder)
    {
        if (!CheckValueData(middleDepositBorder))
            throw new ArgumentException("MiddleDepositBorder is invalid");
        MiddleDepositBorder = middleDepositBorder;
    }

    public void ChangeLimitForSuspicious(decimal limitForSuspicious)
    {
        if (!CheckValueData(limitForSuspicious))
            throw new ArgumentException("LimitForSuspicious is invalid");
        LimitForSuspicious = limitForSuspicious;
    }
}