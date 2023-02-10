using System.Collections.Specialized;
using System.Text.Json;
using System.Text.Json.Serialization;
using Backups.Extra.Entities;
using Microsoft.Extensions.Configuration;
namespace Backups.Extra.Services;

public class JsonParser
{
    private IConfigurationRoot _config;
    public JsonParser()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath("/home/angrydog/Public/С#_labs/AngryDogy/Lab5/Backups.Extra")
            .AddJsonFile("appsettings.json").Build();
    }

    public string GetContentPath()
    {
        string? result = _config.GetValue<string>("ContentPath");
        if (result is null)
        {
            throw new InvalidOperationException("Json file is invalid");
        }

        return result;
    }

    public string GetBackupPath()
    {
        string? result = _config.GetValue<string>("ContentPath");
        if (result is null)
        {
            throw new InvalidOperationException("Json file is invalid");
        }

        return result;
    }

    public List<IContent> GetContent()
    {
        var content = new List<IContent>();
        _config.GetSection("Content");
        return content;
    }

    public BackupExtra GetBackup()
    {
        var backup = new BackupExtra();
        return backup;
    }
}