using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicStore.Entities.Configurations;
using MusicStore.Services.Interfaces;

namespace MusicStore.Services.Implementations;

public class FileUploader : IFileUploader
{
    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<FileUploader> _logger;

    public FileUploader(IOptions<AppSettings> options, ILogger<FileUploader> logger)
    {
        _options = options;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(string base64String, string filePath)
    {
        try
        {
            if (string.IsNullOrEmpty(base64String)) return string.Empty;

            var bytes = Convert.FromBase64String(base64String);

            // C:\\Servidor\Pictures\concierto01.jpg
            var path = Path.Combine(_options.Value.StorageConfiguration.Path, filePath);

            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }
        
            //http://localhost/pictures/concierto01.jpg

            return $"{_options.Value.StorageConfiguration.PublicUrl}{filePath}";

        }
        catch (Exception ex)
        {
          _logger.LogError(ex.Message);

          return string.Empty;
        }
    }
}