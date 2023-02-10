using Banks.Entities;
using Banks.Models;
using Banks.Tools;

namespace Banks.Builder;

public class ClientBuilder : IClientBuilder
{
    private ClientName? _clientName;
    private Address? _address;
    private PassportNumber? _passportNumber;
    private Client? _client;

    public void Reset()
    {
        _client = null;
        _clientName = null;
        _address = null;
        _passportNumber = null;
    }

    public Client GetClient()
    {
        Client? client = _client;
        Reset();
        if (client is null)
        {
            throw new InvalidClientBuilderException("Client is null!");
        }

        return client;
    }

    public void CreateClient(Bank bank)
    {
        if (_clientName is null)
            throw new InvalidClientBuilderException("ClientName is null. Can't create client without ClientName");
        _client = new Client(Guid.NewGuid(), bank, _clientName, _address, _passportNumber);
    }

    public void BuildFullName(string name, string surname)
    {
        _clientName = new ClientName(name, surname);
    }

    public void BuildAddress(string country, string city, string street, int houseNumber)
    {
        _address = new Address(country, city, street, houseNumber);
    }

    public void BuildPassportNumber(long passportNumber)
    {
        _passportNumber = new PassportNumber(passportNumber);
    }
}