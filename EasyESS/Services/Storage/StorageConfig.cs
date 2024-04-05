namespace EasyESS.Services.Storage
{

    public class StorageConfig
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Diagnostics Diagnostics { get; set; }
        public General General { get; set; }
        public Authentication Authentication { get; set; }
        public Obsoletedatacleaning ObsoleteDataCleaning { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
        public bool EnableSettingsLogging { get; set; }
        public bool EnableAuditLogging { get; set; }
        public Filelogoutput FileLogOutput { get; set; }
    }

    public class Filelogoutput
    {
        public string Format { get; set; }
        public string Directory { get; set; }
        public string File { get; set; }
    }

    public class General
    {
        public string FileStoragePath { get; set; }
        public bool EnableScaling { get; set; }
    }

    public class Authentication
    {
        public string TokenIssuer { get; set; }
        public string Audience { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    public class Obsoletedatacleaning
    {
        public object ObsoleteDataCleanInterval { get; set; }
        public object ObsoleteDataLifePeriod { get; set; }
    }

}
