namespace Infraestructure.Helpers
{
    public class AppSecrets
    {
        public EmailSettings EmailSettings { get; set; }
        public NgRok NgRok { get; set; }
        public MercadoPagoConfiguration MercadoPago { get; set; }
        public VitalAPI VitalAPI { get; set; }
        public S3Storage S3Storage { get; set; }
        public Google Google { get; set; }
        public ApplicationLocation ApplicationLocation { get; set; }
        public MongoDatabaseConfig MongoDatabaseConfig { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class EmailSettings
    {
        public string? Server { get; set; }
        public int Port { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class NgRok
    {
        public string NotificationUrl { get; set; }
    }

    public class ConnectionStrings
    {
        public string PostgresDB { get; set; }
    }

    public class MercadoPagoConfiguration
    {
        public string AccessToken { get; set; }
    }

    public class VitalAPI
    {
        public int TimeTokenIsValid { get; set; }
        public string KeySecret { get; set; }
    }

    public class S3Storage
    {
        public string BucketName { get; set; }
    }

    public class Google
    {
        public string client_id { get; set; }
        public string project_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string api_key { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }
        public string javascript_origins { get; set; }
    }

    public class ApplicationLocation
    {
        public string Location { get; set; }
    }
}
