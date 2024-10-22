﻿namespace MusicStore.Services.Interfaces;

public interface IFileUploader
{
    Task<string> UploadFileAsync(string base64String, string filePath);
}
