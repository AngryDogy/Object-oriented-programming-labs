using Banks.Entities;

namespace Banks.Builder;

public class ClientBuilderDirector
{
    public ClientBuilderDirector(IClientBuilder clientBuilder)
    {
        ClientBuilder = clientBuilder;
    }

    public IClientBuilder ClientBuilder { get; }
    public void BuildClient(Bank bank, string name, string surname)
    {
        ClientBuilder.BuildFullName(name, surname);
        ClientBuilder.CreateClient(bank);
    }

    public void BuildClientWithAddress(Bank bank, string name, string surname, string country, string city, string street, int houseNumber)
    {
        ClientBuilder.BuildFullName(name, surname);
        ClientBuilder.BuildAddress(country, city, street, houseNumber);
        ClientBuilder.CreateClient(bank);
    }

    public void BuildClientWithPassportNumber(Bank bank, string name, string surname, long passportNumber)
    {
        ClientBuilder.BuildFullName(name, surname);
        ClientBuilder.BuildPassportNumber(passportNumber);
        ClientBuilder.CreateClient(bank);
    }

    public void BuildFullClient(Bank bank, string name, string surname, string country, string city, string street, int houseNumber, long passportNumber)
    {
        ClientBuilder.BuildFullName(name, surname);
        ClientBuilder.BuildAddress(country, city, street, houseNumber);
        ClientBuilder.BuildPassportNumber(passportNumber);
        ClientBuilder.CreateClient(bank);
    }
}