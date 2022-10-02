using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicStore.Entities.Configurations;
using MusicStore.Services.Interfaces;

namespace MusicStore.Services.Implementations;

public class AzureBlobStorageUploader : IFileUploader
{
    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<AzureBlobStorageUploader> _logger;

    public AzureBlobStorageUploader(IOptions<AppSettings> options, ILogger<AzureBlobStorageUploader> logger)
    {
        _options = options;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(string base64String, string filePath)
    {
        if (string.IsNullOrEmpty(base64String)) return string.Empty;

        try
        {
            var client = new BlobServiceClient(_options.Value.StorageConfiguration.Path);

            var container = client.GetBlobContainerClient("pictures");

            var bobClient = container.GetBlobClient(filePath);

            using (var mem = new MemoryStream(Convert.FromBase64String(base64String)))
            {
                await bobClient.UploadAsync(mem, true);

                return $"{_options.Value.StorageConfiguration.PublicUrl}{filePath}";
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Ocurrio un error al subir el archivo {message}", ex.Message);
            return string.Empty;
        }
    }
}