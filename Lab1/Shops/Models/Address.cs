namespace Shops.Models;

public class Address
{
    private const int Zero = 0;
    public Address(string country, string city, string street, int houseNumber)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            throw new InvalidOperationException("A country is invalid!");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new InvalidOperationException("A city is invalid!");
        }

        if (string.IsNullOrWhiteSpace(street))
        {
            throw new InvalidOperationException("A street is invalid!");
        }

        if (houseNumber < 0)
        {
            throw new InvalidOperationException("houseNumber is negative");
        }

        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public int HouseNumber { get; }
    public override string ToString()
    {
        return Country + ", " + City + ", " + Street + ", " + HouseNumber.ToString();
    }
}