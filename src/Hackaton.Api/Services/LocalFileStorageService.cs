﻿namespace Hackaton.Api.Services;

public class LocalFileStorageService : ILocalFileStorageService
{
    private const string _tempDirectoryName = "tmp";
    private readonly ILogger<LocalFileStorageService> _logger;

    public LocalFileStorageService(ILogger<LocalFileStorageService> logger)
    {
        _logger = logger;

        try
        {
            if (!Directory.Exists(_tempDirectoryName))
            {
                Directory.CreateDirectory(_tempDirectoryName);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to create the {Temp} folder.", _tempDirectoryName);
        }
    }

    public void SaveFile(IFormFile file, Guid fileId)
    {
        var fileStream = new MemoryStream();
        file.CopyTo(fileStream);

        try
        {
            if (!Directory.Exists($"{_tempDirectoryName}/{fileId}"))
            {
                Directory.CreateDirectory($"{_tempDirectoryName}/{fileId}");
            }

            File.WriteAllBytes($"{_tempDirectoryName}/{fileId}/{file.FileName}", fileStream.ToArray());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to create the directory and write the file {File}.", fileId);
        }
    }

    public void DeleteFile(Guid fileId, string fileName)
    {
        if (!File.Exists($"{_tempDirectoryName}/{fileId}/{fileName}"))
        {
            _logger.LogWarning("File {File} not found while trying to delete. Ignoring.", fileId);
            return;
        }

        try
        {
            File.Delete($"{_tempDirectoryName}/{fileId}/{fileName}");
            Directory.Delete($"{_tempDirectoryName}/{fileId}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to delete the file {File} and exclude the directory locally.", fileId);
        }
    }

    public byte[] GetFileById(Guid fileId, string fileName)
    {
        if (!File.Exists($"{_tempDirectoryName}/{fileId}/{fileName}"))
        {
            _logger.LogError("Unable to return file bytes once the file {File} does not exists locally.", fileId);
            throw new ApplicationException($"The file {fileId} does not exists locally");
        }

        try
        {
            return File.ReadAllBytes($"{_tempDirectoryName}/{fileId}/{fileName}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to read the file {File} bytes from local storage.", fileId);
            throw new ApplicationException($"Error while trying to read the file {fileId} bytes from local storage.", e);
        }
    }
}
