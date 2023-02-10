using Backups.Extra.Entities;

namespace Backups.Extra.Services;

public static class Restore
{
    public static void RestorePoint(RestorePointExtra restorePoint, string dirPath)
    {
        if (restorePoint is null)
        {
            throw new NullReferenceException("RestorePoint is null");
        }

        if (string.IsNullOrEmpty(dirPath))
        {
            throw new NullReferenceException("DirPath is null");
        }

        foreach (var storage in restorePoint.Storages)
        {
            string path = Path.Combine(dirPath, Path.GetFileName(storage.Path));
            System.IO.File.Copy(storage.Path, path, true);
        }
    }
}