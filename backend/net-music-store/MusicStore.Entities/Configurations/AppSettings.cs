namespace MusicStore.Entities.Configurations;

public class AppSettings
{
    public StorageConfiguration StorageConfiguration { get; set; }
}

public class StorageConfiguration
{
    public string Path { get; set; }
    public string PublicUrl { get; set; }
}