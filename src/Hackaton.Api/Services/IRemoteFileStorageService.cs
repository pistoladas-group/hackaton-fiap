using Hackaton.Api.Models;

namespace Hackaton.Api.Services;

public interface IRemoteFileStorageService
{
    ExecutionResult ValidateFile(IFormFile file);
    Task<Uri> UploadFileAsync(byte[] file, string fileName, string contentType);
    Task DeleteFileAsync(string fileName);
}
