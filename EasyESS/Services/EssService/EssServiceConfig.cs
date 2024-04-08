namespace EasyESS.Services.EssService
{

    internal class EssServiceConfig
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

    internal class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    internal class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    internal class Connectionstrings
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

    internal class Authentication
    {
        public string TrustedIssuer { get; set; }
        public string Audience { get; set; }
        public string EncryptionKey { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    internal class Facilityflowprocessingservice
    {
        public int ProcessDelayMs { get; set; }
    }

    internal class Cachevalidationservice
    {
        public int ProcessDelayMs { get; set; }
    }

    internal class Globalization
    {
        public string DefaultCulture { get; set; }
    }

    internal class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
        public string HealthCheckTimeout { get; set; }
        public bool EnableConfigLogging { get; set; }
        public bool EnableAuditLogging { get; set; }
        public Filelogoutput FileLogOutput { get; set; }
    }

    internal class Filelogoutput
    {
        public string Format { get; set; }
        public string Directory { get; set; }
        public string File { get; set; }
    }

}
