namespace Infraestructure.Helpers
{
    public static class Base64FileConverter
    {
        public static string ConvertToBase64(Stream fileStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}
