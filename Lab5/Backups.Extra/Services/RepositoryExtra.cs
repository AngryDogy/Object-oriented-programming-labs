using System.IO.Compression;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Backups.Extra.Entities;
using Backups.Extra.Tools;

namespace Backups.Extra.Services;

public class RepositoryExtra : IRepositoryExtra
{
    private List<IContent> _content;
    private List<ILimiter>? _limiters;
    private LoggingService? _loggingSerivce;

    public RepositoryExtra(JsonParser parser)
    {
        ContentPath = parser.GetContentPath();
        BackupPath = parser.GetBackupPath();
        Backup = parser.GetBackup();
        _content = parser.GetContent();
    }

    public RepositoryExtra(string contentPath, string backupPath)
    {
        if (string.IsNullOrEmpty(contentPath))
        {
            throw new NullReferenceException("ContentPath is null");
        }

        if (string.IsNullOrEmpty(backupPath))
        {
            throw new NullReferenceException("BackupPath is null");
        }

        ContentPath = contentPath;
        BackupPath = backupPath;
        Directory.CreateDirectory(BackupPath);
        Directory.CreateDirectory(ContentPath);
        _content = new List<IContent>();
        Backup = new BackupExtra();
    }

    public bool AreAllLimitersRequiredToDelete { get; private set; }
    public IReadOnlyList<IContent> Content => _content.AsReadOnly();
    public string ContentPath { get; }
    public string BackupPath { get; }
    public BackupExtra Backup { get; }

    public void AddLimiters(List<ILimiter> limiters, bool areAllLimitersRequiredToDelete)
    {
        if (limiters is null)
        {
            throw new NullReferenceException("List is null");
        }

        _limiters = limiters;
        AreAllLimitersRequiredToDelete = areAllLimitersRequiredToDelete;
    }

    public void AddLoggingService(LoggingService loggingService)
    {
        if (loggingService is null)
        {
            throw new NullReferenceException("Logging service is null");
        }

        _loggingSerivce = loggingService;
    }

    public void Save()
    {
        string json = JsonSerializer.Serialize(this);
        System.IO.File.WriteAllText("/home/angrydog/Public/С#_labs/AngryDogy/Lab5/Backups.Extra/appsettings.json", json);
    }

    public void AddContent(IContent content)
    {
        if (content is null)
        {
            throw new NullReferenceException("Content is null");
        }

        _content.Add(content);
    }

    public void AddRestorePoint(RestorePointExtra restorePoint)
    {
        if (restorePoint is null)
        {
            throw new NullReferenceException("Restore point is null");
        }

        Backup.RestorePoints.Add(restorePoint);
        CreateRestorePoint(restorePoint);
        _loggingSerivce?.Log($"Restore point was created! Id: {restorePoint.Id}, date of creation: {restorePoint.DateOfCreation}");
        if (_limiters != null)
        {
            List<RestorePointExtra> toDelete;
            if (AreAllLimitersRequiredToDelete)
                toDelete = CheckLimitsIntersectPoints();
            else
                toDelete = CheckLimitsAllPoints();
            if (toDelete.Count != 0)
                DeleteRestorePoints(toDelete);
        }
    }

    public void Merge(RestorePointExtra oldPoint, RestorePointExtra newPoint)
    {
        if (oldPoint is null)
        {
            throw new NullReferenceException("OldPoint is null");
        }

        if (newPoint is null)
        {
            throw new NullReferenceException("NewPoint is null");
        }

        foreach (StorageExtra oldStorage in oldPoint.Storages)
        {
            bool check = true;
            foreach (StorageExtra newPointStorage in newPoint.Storages)
            {
                var differences = oldStorage.Storage.Content.Except(newPointStorage.Storage.Content).ToList();
                if (differences.Count == 0)
                {
                    check = false;
                }
            }

            if (check)
            {
                oldStorage.ChangePath(Path.Combine(newPoint.Path, $"Storage {newPoint.Storages.Count}"));
                newPoint.AddStorage(oldStorage);
            }
        }

        CreateRestorePoint(newPoint);
        DeleteRestorePoint(oldPoint);
    }

    public RestorePointExtra GetRestorePoint(int id)
    {
        RestorePointExtra? restorePoint = Backup.RestorePoints.SingleOrDefault(x => x.Id == id);
        if (restorePoint is null)
            throw new InvalidBackupsExtraOperation("Can't find a restore point");
        return restorePoint;
    }

    private void CreateRestorePoint(RestorePointExtra restorePoint)
    {
        if (restorePoint is null)
        {
            throw new NullReferenceException("restorePoint is null!");
        }

        string pathToRestorePoint = restorePoint.Path;
        Directory.CreateDirectory(pathToRestorePoint);
        int storageId = 0;
        foreach (StorageExtra storage in restorePoint.Storages)
        {
            string storageName = "Storage " + storageId.ToString();
            string pathToStorage = storage.Path;
            storageId++;
            using (FileStream zipToOpen = new FileStream(pathToStorage, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    CreateArchive(storage, archive);
                }
            }
        }
    }

    private void CreateArchive(StorageExtra storage, ZipArchive archive)
    {
        foreach (var obj in storage.Storage.Content)
        {
            if (obj.Type == "file")
            {
                archive.CreateEntryFromFile(obj.Path, obj.Name);
            }
            else
            {
                string pathToZip = Path.Combine(BackupPath, "zip");
                ZipFile.CreateFromDirectory(obj.Path, pathToZip);
                using (FileStream dirZip = new FileStream(pathToZip, FileMode.Open))
                {
                    using (ZipArchive dirArcive = new ZipArchive(dirZip, ZipArchiveMode.Read))
                    {
                        archive.CreateEntryFromFile(pathToZip, obj.Name);
                    }
                }

                System.IO.File.Delete(pathToZip);
            }
        }
    }

    private List<RestorePointExtra> CheckLimitsIntersectPoints()
    {
        if (_limiters is null)
        {
            throw new InvalidBackupsExtraOperation("Can't check limits. Limiters don't exist");
        }

        var toDelete = new List<RestorePointExtra>();
        foreach (ILimiter limiter in _limiters)
        {
            if (toDelete.Count == 0)
            {
                toDelete = limiter.GetRestorePointsToDelete(this);
            }
            else
            {
                toDelete = (List<RestorePointExtra>)toDelete.Intersect(limiter.GetRestorePointsToDelete(this));
            }
        }

        return toDelete;
    }

    private List<RestorePointExtra> CheckLimitsAllPoints()
    {
        if (_limiters is null)
        {
            throw new InvalidBackupsExtraOperation("Can't check limits. Limiters don't exist");
        }

        var toDelete = new List<RestorePointExtra>();
        foreach (ILimiter limiter in _limiters)
        {
            toDelete.AddRange(limiter.GetRestorePointsToDelete(this));
        }

        return toDelete;
    }

    private void DeleteRestorePoint(RestorePointExtra restorePoint)
    {
        if (restorePoint is null)
        {
            throw new NullReferenceException("RestorePoint is null");
        }

        if (Backup.RestorePoints.Contains(restorePoint))
        {
            _loggingSerivce?.Log($"Restore point with id: {restorePoint.Id} was deleted");
            System.IO.Directory.Delete(restorePoint.Path, true);
            Backup.RestorePoints.Remove(restorePoint);
        }
    }

    private void DeleteRestorePoints(List<RestorePointExtra> restorePoints)
    {
        if (restorePoints is null)
        {
            throw new NullReferenceException("RestorePoint is null");
        }

        foreach (RestorePointExtra restorePoint in restorePoints)
        {
            if (Backup.RestorePoints.Contains(restorePoint))
            {
                _loggingSerivce?.Log($"Restore point with id {restorePoint.Id} was deleted");
                System.IO.Directory.Delete(restorePoint.Path, true);
                Backup.RestorePoints.Remove(restorePoint);
            }
        }
    }
}