using Backups.Extra.Services;

namespace Backups.Extra.Entities;

public class BackupTaskExtra
{
    private int _idCounter = 0;
    private IRepositoryExtra _repository;
    private List<BackupObject> _backupObjects;
    private IStorageAlgorithm _storageAlgorithm;
    public BackupTaskExtra(IRepositoryExtra repository, IStorageAlgorithm storageAlgorithm)
    {
        if (repository is null)
        {
            throw new NullReferenceException("repository is null!");
        }

        if (storageAlgorithm is null)
        {
            throw new NullReferenceException("storageAlgorithm is null");
        }

        _repository = repository;
        _backupObjects = new List<BackupObject>();
        _storageAlgorithm = storageAlgorithm;
    }

    public void AddBackupObjects(IReadOnlyCollection<IContent> content)
    {
        if (content is null)
        {
            throw new NullReferenceException("content is null!");
        }

        var backupObjects = new List<BackupObject>();
        foreach (IContent obj in content)
        {
            backupObjects.Add(new BackupObject(obj));
        }

        _backupObjects = backupObjects;
    }

    public void DeleteBackupObjects(List<IContent> content)
    {
        if (content is null)
        {
            throw new NullReferenceException("content is null!");
        }

        foreach (IContent obj in content)
        {
            BackupObject? backupObject = _backupObjects.SingleOrDefault(x => x.ObjectInfo == obj);
            if (backupObject != null)
            {
                _backupObjects.Remove(backupObject);
            }
            else
            {
                throw new InvalidBackupOperationException("BackupOjbect doens't exist! Can't delete it!");
            }
        }
    }

    public void CreateRestorePoint()
    {
        var extraStorages = new List<StorageExtra>();
        List<Storage> storages = _storageAlgorithm.CreateStorages(_backupObjects);
        string pathToRestorePoint = Path.Combine(_repository.BackupPath, $"Restore point {_idCounter}");
        for (int i = 0; i < storages.Count; i++)
        {
            string pathToStorage = Path.Combine(pathToRestorePoint, $"Storage {i}");
            extraStorages.Add(new StorageExtra(storages[i], pathToStorage));
        }

        var restorePoint = new RestorePointExtra(_idCounter, pathToRestorePoint, extraStorages, DateTime.Now);
        _idCounter++;
        _repository.AddRestorePoint(restorePoint);
    }
}