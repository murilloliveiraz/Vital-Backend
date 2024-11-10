using Infraestructure.Services.Helpers;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Services.Interfaces
{
    public interface IS3StorageService
    {
        Task<S3UploadResult> UploadFileAsync(IFormFile file, string prefix, string bucketName);
        Task<S3FileResponse> GetFileByKeyAsync(string key, string bucketName);
    }
}
