namespace EasyESS.Services.Document
{

    internal class DocumentConfig
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Globalization Globalization { get; set; }
        public Diagnostics Diagnostics { get; set; }
        public Authentication Authentication { get; set; }
        public Caching Caching { get; set; }
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
        public string IdentityService { get; set; }
        public string StorageService { get; set; }
    }

    internal class Globalization
    {
        public string DefaultCulture { get; set; }
    }

    internal class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
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

    internal class Authentication
    {
        public string TrustedIssuer { get; set; }
        public string Audience { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    internal class Caching
    {
        public string CacheDirectory { get; set; }
        public string CacheLifetime { get; set; }
        public int InMemoryCacheSizeMB { get; set; }
    }

}
