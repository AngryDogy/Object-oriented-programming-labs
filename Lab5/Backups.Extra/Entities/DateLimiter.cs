using Backups.Extra.Services;

namespace Backups.Extra.Entities;

public class DateLimiter : ILimiter
{
    private double _daysLimit;
    public DateLimiter(double daysLimit)
    {
        if (daysLimit < 0)
        {
            throw new ArgumentException("DaysLimit can't be negative");
        }

        _daysLimit = daysLimit;
    }

    public List<RestorePointExtra> GetRestorePointsToDelete(IRepositoryExtra repository)
    {
        if (repository is null)
        {
            throw new NullReferenceException("Repository is null");
        }

        var toDelete = new List<RestorePointExtra>();
        foreach (RestorePointExtra restorePoint in repository.Backup.RestorePoints)
        {
            double difference = (DateTime.Now - restorePoint.DateOfCreation).TotalDays;
            if (difference > _daysLimit)
            {
                toDelete.Add(restorePoint);
            }
        }

        return toDelete;
    }
}