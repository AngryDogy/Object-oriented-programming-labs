namespace Banks.Models;

public class ClientName
{
    public ClientName(string name, string surname)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new NullReferenceException("name is null");
        }

        if (string.IsNullOrEmpty(surname))
        {
            throw new NullReferenceException("surname is null");
        }

        Name = name;
        Surname = surname;
    }

    public string Name { get; }
    public string Surname { get; }
    public override string ToString()
    {
        return Name + " " + Surname;
    }
}