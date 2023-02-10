using Backups.Extra.Services;

namespace Backups.Extra.Entities;

public class AmountLimiter : ILimiter
{
    public AmountLimiter(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount is invalid");
        }

        Amount = amount;
    }

    public int Amount { get; }
    public List<RestorePointExtra> GetRestorePointsToDelete(IRepositoryExtra repository)
    {
        if (repository is null)
        {
            throw new NullReferenceException("Repository is null");
        }

        int k = repository.Backup.RestorePoints.Count - Amount;
        var toDelete = new List<RestorePointExtra>();
        for (int i = 0; i < k; i++)
        {
            toDelete.Add(repository.Backup.RestorePoints[i]);
        }

        return toDelete;
    }
}