namespace Shops.Entities;

public class Person
{
    private const int Zero = 0;
    public Person(string name, decimal money)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("A name is invalid!");
        }

        if (money < Zero)
        {
            throw new InvalidOperationException("money is negative");
        }

        Name = name;
        Money = money;
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public decimal Money { get; private set; }
    public Guid Id { get; }
    public void Pay(decimal money)
    {
        if (money < Zero)
        {
            throw new InvalidOperationException("money is negative");
        }

        if (Money < money)
        {
            throw new InvalidOperationException("A person doesn't have enough money!");
        }

        Money = Money - money;
    }
}