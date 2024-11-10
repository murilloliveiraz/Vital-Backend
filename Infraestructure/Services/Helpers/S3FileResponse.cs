namespace Infraestructure.Services.Helpers
{
    public class S3FileResponse
    {
        public string? Name { get; set; }
        public string? PresignedUrl { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
