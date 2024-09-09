namespace Infraestructure.Services.Helpers
{
    public class S3UploadResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Key { get; set; }
    }
}
