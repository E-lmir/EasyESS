namespace EasyESS.Services.MessageBroker.WebApiService
{

    internal class WebApiConfig
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

    internal class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    internal class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftEntityFrameworkCore { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    internal class Diagnostics
    {
        internal bool EnableRequestProfiling { get; set; }
        internal bool EnableConfigLogging { get; set; }
        internal bool EnableAuditLogging { get; set; }
        internal string LogOutputs { get; set; }
        internal Filelogoutput FileLogOutput { get; set; }
        internal Elasticsearchlogoutput ElasticSearchLogOutput { get; set; }
    }

    internal class Filelogoutput
    {
        public string Format { get; set; }
        public string Directory { get; set; }
        public string File { get; set; }
    }

    internal class Elasticsearchlogoutput
    {
        public string ServiceAddress { get; set; }
        public string Index { get; set; }
        public string ApiKeyId { get; set; }
        public string ApiKey { get; set; }
    }

    internal class Connectionstrings
    {
        public string Database { get; set; }
    }

    internal class Secrets
    {
        public Database Database { get; set; }
    }

    internal class Database
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    internal class Authentication
    {
        public string TrustedIssuer { get; set; }
        public string Audience { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    internal class Messaging
    {
        public string DefaultMessageExpirationPeriod { get; set; }
    }

    internal class Scheduler
    {
        public string HealthCheckUrl { get; set; }
    }

}
