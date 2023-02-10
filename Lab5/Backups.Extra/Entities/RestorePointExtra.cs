namespace Backups.Extra.Entities;

public class RestorePointExtra
{
    private List<StorageExtra> _storages;
    public RestorePointExtra(int id, string path, List<StorageExtra> storages, DateTime dateOfCreation)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new NullReferenceException("Path is null");
        }

        if (storages is null)
        {
            throw new NullReferenceException("List of storages is null");
        }

        Id = id;
        Path = path;
        _storages = storages;
        DateOfCreation = dateOfCreation;
    }

    public int Id { get; }
    public string Path { get; }
    public DateTime DateOfCreation { get;  }
    public IReadOnlyCollection<StorageExtra> Storages => _storages.AsReadOnly();

    public void AddStorage(StorageExtra storage)
    {
        if (storage is null)
        {
            throw new NullReferenceException("Storage is null");
        }

        _storages.Add(storage);
    }
}