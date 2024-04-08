namespace EasyESS.Services.MessageBroker.Scheduler
{
    internal class SchedulerConfig
    {
        public Logging Logging { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Secrets Secrets { get; set; }
        public Diagnostics Diagnostics { get; set; }
        public Transport Transport { get; set; }
        public string[] RetryPolicyIntervals { get; set; }
        public Messageprocessingservice MessageProcessingService { get; set; }
        public Testchecks TestChecks { get; set; }
    }

    internal class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    internal class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftEntityFrameworkCore { get; set; }
        public string SystemNetHttpHttpClientDefault { get; set; }
        public string MicrosoftAspNetCore { get; set; }
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

    internal class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
        public bool EnableConfigLogging { get; set; }
    }

    internal class Transport
    {
        public string SmsDeliveryProxy { get; set; }
        public Proxy[] Proxies { get; set; }
    }

    internal class Proxy
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Configuration Configuration { get; set; }
    }

    internal class Configuration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Sender { get; set; }
        public float[] LimitProportionByPriority { get; set; }
        public int MaxTransmitRetryCount { get; set; }
        public bool UseTinyUrl { get; set; }
        public int MessagesPerSecond { get; set; }
        public string FromAddress { get; set; }
        public string FromContact { get; set; }
        public string ApiKey { get; set; }
        public string Path { get; set; }
    }

    internal class Messageprocessingservice
    {
        public int ProcessDelayMs { get; set; }
    }

    internal class Testchecks
    {
        public Check[] Checks { get; set; }
    }

    internal class Check
    {
        public string name { get; set; }
        public string type { get; set; }
        public Configuration1 configuration { get; set; }
    }

    internal class Configuration1
    {
        public string[] ValidatingFiles { get; set; }
        public Expectedvalues ExpectedValues { get; set; }
        public string configFileName { get; set; }
        public string[] RequiredFiles { get; set; }
        public string[] RequiredAuthentications { get; set; }
        public string healthEndpoint { get; set; }
    }

    internal class Expectedvalues
    {
        public string ConnectionStringsDatabase { get; set; }
        public string AuthenticationSigningCertificateThumbprint { get; set; }
        public string TransportProxies0ConfigurationHost { get; set; }
        public string TransportProxies0ConfigurationPort { get; set; }
        public string TransportProxies0ConfigurationUseSsl { get; set; }
        public string TransportProxies0ConfigurationUsername { get; set; }
        public string TransportProxies0ConfigurationPassword { get; set; }
        public string TransportProxies0ConfigurationSender { get; set; }
    }
}
