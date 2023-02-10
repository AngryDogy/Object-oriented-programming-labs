using Backups.Extra.Entities;
using Backups.Extra.Services;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Backups.Extra.Test;

public class BackupTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BackupTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact(Skip = "Skipped due to prohibition of working with files")]
    public void SaveAppSettings()
    {
        var repository = new RepositoryExtra("/home/angrydog/repository", "/home/angrydog/backup");
        var file = new File(new FileInfo("/home/angrydog/Alan/thoughts"));
        var dir = new Folder(new DirectoryInfo("/home/angrydog/Alan/weapon"));
        repository.AddContent(file);
        repository.AddContent(dir);
        var backupTask = new BackupTaskExtra(repository, new SplitStorage());
        backupTask.AddBackupObjects(repository.Content);
        backupTask.CreateRestorePoint();
        repository.Save();
        Assert.True(System.IO.File.Exists("/home/angrydog/Public/С#_labs/AngryDogy/Lab5/Backups.Extra/appsettings.json"));
    }

    [Fact(Skip = "Skipped due to prohibition of working with files")]
    public void CheckLogging()
    {
        LoggingService loggingService =
            new FileLoggingService(new Logger(true), "/home/angrydog/Public/С#_labs/AngryDogy/Lab5/Backups.Extra/log.txt");
        var repository = new RepositoryExtra("/home/angrydog/repository", "/home/angrydog/backup");
        repository.AddLoggingService(loggingService);
        var file = new File(new FileInfo("/home/angrydog/Alan/thoughts"));
        var dir = new Folder(new DirectoryInfo("/home/angrydog/Alan/weapon"));
        repository.AddContent(file);
        repository.AddContent(dir);
        var backupTask = new BackupTaskExtra(repository, new SplitStorage());
        backupTask.AddBackupObjects(repository.Content);
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        Assert.True(System.IO.File.Exists("/home/angrydog/Public/С#_labs/AngryDogy/Lab5/Backups.Extra/log.txt"));
    }

    [Fact(Skip = "Skipped due to prohibition of working with files")]
    public void GetAppSettings()
    {
        var repository = new RepositoryExtra(new JsonParser());
        Assert.True(repository.BackupPath == "/home/angrydog/backup");
    }

    [Fact(Skip = "Skipped due to prohibition of working with files")]
    public void CheckLimiters()
    {
        var limiters = new List<ILimiter>();
        limiters.Add(new AmountLimiter(2));
        limiters.Add(new DateLimiter(1));
        var repository = new RepositoryExtra("/home/angrydog/repository", "/home/angrydog/backup");
        repository.AddLimiters(limiters, false);
        var file = new File(new FileInfo("/home/angrydog/Alan/thoughts"));
        var dir = new Folder(new DirectoryInfo("/home/angrydog/Alan/weapon"));
        repository.AddContent(file);
        repository.AddContent(dir);
        var backupTask = new BackupTaskExtra(repository, new SplitStorage());
        backupTask.AddBackupObjects(repository.Content);
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        Assert.True(repository.Backup.RestorePoints.Count == 2);
    }

    [Fact(Skip = "Skipped due to prohibition of working with files")]
    public void CheckMerge()
    {
        LoggingService loggingService =
            new FileLoggingService(new Logger(true), "/home/angrydog/Public/С#_labs/AngryDogy/Lab5/Backups.Extra/log.txt");
        var repository = new RepositoryExtra("/home/angrydog/repository", "/home/angrydog/backup");
        repository.AddLoggingService(loggingService);
        var file = new File(new FileInfo("/home/angrydog/Alan/thoughts"));
        var dir = new Folder(new DirectoryInfo("/home/angrydog/Alan/weapon"));
        var file2 = new File(new FileInfo("/home/angrydog/Alan/money"));
        repository.AddContent(file);
        repository.AddContent(dir);
        var backupTask = new BackupTaskExtra(repository, new SplitStorage());
        backupTask.AddBackupObjects(repository.Content);
        backupTask.CreateRestorePoint();

        repository.AddContent(file2);
        backupTask.AddBackupObjects(repository.Content);
        backupTask.CreateRestorePoint();
        var point1 = repository.GetRestorePoint(0);

        var point2 = repository.GetRestorePoint(1);
        repository.Merge(point2, point1);

        Assert.True(System.IO.File.Exists("/home/angrydog/backup/Restore point 0/Storage 2"));
    }

    [Fact(Skip = "Skipped due to prohibition of working with files")]
    public void Restore()
    {
        var repository = new RepositoryExtra("/home/angrydog/repository", "/home/angrydog/backup");
        var file = new File(new FileInfo("/home/angrydog/Alan/thoughts"));
        var dir = new Folder(new DirectoryInfo("/home/angrydog/Alan/weapon"));
        repository.AddContent(file);
        repository.AddContent(dir);
        var backupTask = new BackupTaskExtra(repository, new SplitStorage());
        backupTask.AddBackupObjects(repository.Content);
        backupTask.CreateRestorePoint();
        var point = repository.GetRestorePoint(0);
        Services.Restore.RestorePoint(point, repository.ContentPath);
    }
}