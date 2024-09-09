namespace Infraestructure.Services.Helpers
{
    public class S3FileResult
    {
        public bool Success { get; set; }
        public Stream Stream { get; set; }
        public string ContentType { get; set; }
        public string Message { get; set; }
    }
}
