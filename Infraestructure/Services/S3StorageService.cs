using Amazon.S3;
using Amazon.S3.Model;
using Infraestructure.Services.Helpers;
using Infraestructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infraestructure.Services
{
    public class S3StorageService : IS3StorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly S3StorageOptions _options;

        public S3StorageService(IAmazonS3 s3Client, IOptions<S3StorageOptions> options)
        {
            _s3Client = s3Client;
            _options = options.Value;
        }

        public async Task<S3UploadResult> UploadFileAsync(IFormFile file, string prefix)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _options.BucketName);
            if (!bucketExists)
            {
                return new S3UploadResult { Success = false, Message = "Bucket does not exist." };
            }

            var key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}";
            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(request);
            return new S3UploadResult { Success = true, Message = $"File {key} uploaded to S3 successfully!", Key = key };
        }
        
        public async Task<S3FileResult> GetFileByKeyAsync(string key)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _options.BucketName);
            if (!bucketExists)
            {
                return new S3FileResult { Success = false, Message = "Bucket does not exist." };
            }

            var s3Object = await _s3Client.GetObjectAsync(_options.BucketName, key);
            return new S3FileResult { Success = true, Stream = s3Object.ResponseStream, ContentType = s3Object.Headers.ContentType };
        }
    }
}
