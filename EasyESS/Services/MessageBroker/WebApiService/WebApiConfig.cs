namespace EasyESS.Services.MessageBroker.WebApiService
{

    public class WebApiConfig
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Diagnostics Diagnostics { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Secrets Secrets { get; set; }
        public Authentication Authentication { get; set; }
        public Messaging Messaging { get; set; }
        public Scheduler Scheduler { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftEntityFrameworkCore { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
        public bool EnableConfigLogging { get; set; }
        public bool EnableAuditLogging { get; set; }
        public string LogOutputs { get; set; }
        public Filelogoutput FileLogOutput { get; set; }
        public Elasticsearchlogoutput ElasticSearchLogOutput { get; set; }
    }

    public class Filelogoutput
    {
        public string Format { get; set; }
        public string Directory { get; set; }
        public string File { get; set; }
    }

    public class Elasticsearchlogoutput
    {
        public string ServiceAddress { get; set; }
        public string Index { get; set; }
        public string ApiKeyId { get; set; }
        public string ApiKey { get; set; }
    }

    public class Connectionstrings
    {
        public string Database { get; set; }
    }

    public class Secrets
    {
        public Database Database { get; set; }
    }

    public class Database
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Authentication
    {
        public string TrustedIssuer { get; set; }
        public string Audience { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    public class Messaging
    {
        public string DefaultMessageExpirationPeriod { get; set; }
    }

    public class Scheduler
    {
        public string HealthCheckUrl { get; set; }
    }

}
