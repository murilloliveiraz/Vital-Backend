using Amazon.S3;
using Amazon.S3.Model;
using Infraestructure.Services.Helpers;
using Infraestructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Services
{
    public class S3StorageService : IS3StorageService
    {
        private readonly IAmazonS3 _s3Client;

        public S3StorageService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<S3UploadResult> UploadFileAsync(IFormFile file, string prefix, string bucketName)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists)
            {
                return new S3UploadResult { Success = false, Message = "Bucket does not exist." };
            }

            var key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}";
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(request);
            return new S3UploadResult { Success = true, Message = $"File {key} uploaded to S3 successfully!", Key = key };
        }
        
        public async Task<S3FileResponse> GetFileByKeyAsync(string key, string bucketName)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists)
            {
                return new S3FileResponse { Success = false, Message = "Bucket does not exists"};
            }
            var urlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };
            return new S3FileResponse()
            {  
                Name = "download",
                PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                Message = "Sucess",
                Success = true
            };
        }
    }
}
