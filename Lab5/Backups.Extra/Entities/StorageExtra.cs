namespace Backups.Extra.Entities;

public class StorageExtra
{
    public StorageExtra(Storage storage, string path)
    {
        if (storage is null)
        {
            throw new NullReferenceException("content is null");
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new NullReferenceException("Path is null");
        }

        Storage = storage;
        Path = path;
    }

    public Storage Storage { get; }
    public string Path { get; private set; }

    public void ChangePath(string newPath)
    {
        if (string.IsNullOrEmpty(newPath))
        {
            throw new NullReferenceException("NewPath is null");
        }

        Path = newPath;
    }
}