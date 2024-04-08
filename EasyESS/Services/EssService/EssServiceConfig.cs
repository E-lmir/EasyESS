namespace EasyESS.Services.EssService
{

    public class EssServiceConfig
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Authentication Authentication { get; set; }
        public Facilityflowprocessingservice FacilityFlowProcessingService { get; set; }
        public Cachevalidationservice CacheValidationService { get; set; }
        public Globalization Globalization { get; set; }
        public Diagnostics Diagnostics { get; set; }
        public string[] RetryPolicyIntervals { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Connectionstrings
    {
        public string Database { get; set; }
        public string RabbitMQ { get; set; }
        public string IdentityService { get; set; }
        public string StorageService { get; set; }
        public string SignService { get; set; }
        public string PreviewService { get; set; }
        public string PreviewStorage { get; set; }
        public string MessagingService { get; set; }
        public string DocumentService { get; set; }
    }

    public class Authentication
    {
        public string TrustedIssuer { get; set; }
        public string Audience { get; set; }
        public string EncryptionKey { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    public class Facilityflowprocessingservice
    {
        public int ProcessDelayMs { get; set; }
    }

    public class Cachevalidationservice
    {
        public int ProcessDelayMs { get; set; }
    }

    public class Globalization
    {
        public string DefaultCulture { get; set; }
    }

    public class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
        public string HealthCheckTimeout { get; set; }
        public bool EnableConfigLogging { get; set; }
        public bool EnableAuditLogging { get; set; }
        public Filelogoutput FileLogOutput { get; set; }
    }

    public class Filelogoutput
    {
        public string Format { get; set; }
        public string Directory { get; set; }
        public string File { get; set; }
    }

}
