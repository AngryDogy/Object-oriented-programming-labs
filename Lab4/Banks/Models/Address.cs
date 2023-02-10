namespace Banks.Models;

public class Address
{
    public Address(string country, string city, string street, int houseNumber)
    {
        if (string.IsNullOrEmpty(country))
        {
            throw new NullReferenceException("country is null");
        }

        if (string.IsNullOrEmpty(city))
        {
            throw new NullReferenceException("city is null");
        }

        if (string.IsNullOrEmpty(street))
        {
            throw new NullReferenceException("street is null");
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
}