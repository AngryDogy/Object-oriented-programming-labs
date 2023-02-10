using Banks.Entities;

namespace Banks.Interfaces;

public interface ITransaction
{
    Guid Id { get; }
    decimal Money { get; }
    IAccount Account { get; }
    bool WasCanceled { get; }
    void Cancel();
}