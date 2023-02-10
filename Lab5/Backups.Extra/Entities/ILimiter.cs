using Backups.Extra.Services;

namespace Backups.Extra.Entities;

public interface ILimiter
{
    List<RestorePointExtra> GetRestorePointsToDelete(IRepositoryExtra repository);
}