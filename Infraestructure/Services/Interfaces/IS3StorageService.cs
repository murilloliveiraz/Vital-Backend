using Infraestructure.Services.Helpers;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Services.Interfaces
{
    public interface IS3StorageService
    {
        Task<S3UploadResult> UploadFileAsync(IFormFile file, string prefix);
        Task<S3FileResult> GetFileByKeyAsync(string key);
    }
}
