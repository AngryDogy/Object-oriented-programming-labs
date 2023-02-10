using Backups.Extra.Entities;

namespace Backups.Extra.Services;

public interface IRepositoryExtra
{
    IReadOnlyList<IContent> Content { get; }
    string ContentPath { get; }
    string BackupPath { get; }
    BackupExtra Backup { get; }
    void AddContent(IContent content);

    void AddRestorePoint(RestorePointExtra restorePoint);
}