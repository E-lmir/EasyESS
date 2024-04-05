using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Services.Document
{

    public class DocumentConfig
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
        public string IdentityService { get; set; }
        public string StorageService { get; set; }
    }

    public class Globalization
    {
        public string DefaultCulture { get; set; }
    }

    public class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
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

    public class Authentication
    {
        public string TrustedIssuer { get; set; }
        public string Audience { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    public class Caching
    {
        public string CacheDirectory { get; set; }
        public string CacheLifetime { get; set; }
        public int InMemoryCacheSizeMB { get; set; }
    }

}
