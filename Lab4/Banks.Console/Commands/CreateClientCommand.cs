using System.Security.Principal;
using Banks.Entities;

namespace Banks.Console;

public class CreateClientCommand : Command
{
    public override void Execute()
    {
        int bankId = GetIntValue("Client's bank Id: ");
        Bank bank = CentralBank.GetBank(IdChanger.OldId[bankId]);
        string name = GetStringValue("Client name: ");
        string surname = GetStringValue("Client surname: ");
        bool addressData = false, passportNumberData = false;
        string country = string.Empty, city = string.Empty, street = string.Empty;
        int houseNumber = -1;
        long passportNumber = -1;
        if (MakeDecision("Does the client have an address? "))
        {
            country = GetStringValue("Country: ");
            city = GetStringValue("City: ");
            street = GetStringValue("Street: ");
            houseNumber = GetIntValue("HouseNumber: ");
            addressData = true;
        }

        if (MakeDecision("Does the client have a passport number?"))
        {
            passportNumber = GetLongValue("PassportNumber: ");
            passportNumberData = true;
        }

        Client client;
        if (passportNumberData && addressData)
        {
            client = CentralBank.AddClient(bank, name, surname, country, city, street, houseNumber, passportNumber);
        }
        else
        {
            if (passportNumberData)
            {
                client = CentralBank.AddClient(bank, name, surname, passportNumber);
            }
            else
            {
                if (addressData)
                {
                    client = CentralBank.AddClient(bank, name, surname, country, city, street, houseNumber);
                }
                else
                {
                    client = CentralBank.AddClient(bank, name, surname);
                }
            }
        }

        IdChanger.ChangeId(client.Id);
        System.Console.WriteLine($"{client.ClientName.ToString()} was registered. Id: {IdChanger.NewId[client.Id]}");
    }
}