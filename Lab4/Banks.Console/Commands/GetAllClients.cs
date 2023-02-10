using Banks.Entities;

namespace Banks.Console;

public class GetAllClients : Command
{
    public override void Execute()
    {
        foreach (Bank bank in CentralBank.Banks)
        {
            foreach (Client client in bank.Clients)
            {
                System.Console.WriteLine($"Name: {client.ClientName.ToString()}, Id: {IdChanger.NewId[client.Id]}");
            }
        }
    }
}