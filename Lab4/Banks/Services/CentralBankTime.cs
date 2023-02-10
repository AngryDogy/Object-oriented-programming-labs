namespace Banks.Services;

public class CentralBankTime
{
    private static CentralBankTime? _instance;
    private CentralBank? _bank;
    private CentralBankTime() { }

    public int CurrentTime { get; private set; }
    public static CentralBankTime GetInstance()
    {
        if (_instance is null)
        {
            _instance = new CentralBankTime();
        }

        return _instance;
    }

    public void IncreaseTime(int time)
    {
        if (time < 0)
        {
            throw new ArgumentException("Time can't be negative");
        }

        CurrentTime += time;
        _bank?.InterestPayment(time);
    }

    public void AddCentralBank(CentralBank bank)
    {
        if (bank is null)
        {
            throw new NullReferenceException("Bank is null");
        }

        _bank = bank;
    }
}