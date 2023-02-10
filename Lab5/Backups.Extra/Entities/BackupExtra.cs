namespace Backups.Extra.Entities;

public class BackupExtra : Backup
{
    public BackupExtra()
    {
        RestorePoints = new List<RestorePointExtra>();
    }

    public new List<RestorePointExtra> RestorePoints { get;  }
}