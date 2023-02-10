using Banks.Entities;

namespace Banks.Builder;

public interface IClientBuilder
{
    public void CreateClient(Bank bank);

    public void BuildFullName(string name, string surname);

    public void BuildAddress(string country, string city, string street, int houseNumber);

    public void BuildPassportNumber(long passportNumber);
    Client GetClient();
}