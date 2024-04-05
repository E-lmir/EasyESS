using Newtonsoft.Json;

namespace EasyESS
{
    public sealed class InstallationInfo
    {
        public string InstanceFolder { get; set; }
        public string InstanceTag { get; set; }
        public string SQLCmdPath { get; set; }
        public string DBServerName { get; set; }
        public string DBServerUser { get; set; }
        public string DBServerPassword { get; set; }
        public string SigningCertificateThumbprint { get; set; }
        public string IntegrationServiceEndpoint { get; set; }
        public string IntegrationServiceUser { get; set; }
        public string IntegrationServicePassword { get; set; }
        public string EssPlatformVersion { get; set; }
        public IdentityServiceInfo IdentityServiceInfo { get; set; }
        public MessagingServiceInfo MessagingServiceInfo { get; set; }
        public IdCLIServiceInfo IdCLIServiceInfo { get; set; }
        public StorageServiceInfo StorageServiceInfo { get; set; }
        public DocumentServiceInfo DocumentServiceInfo { get; set; }
        public SignServiceInfo SignServiceInfo { get; set; }
        public EssServiceInfo EssServiceInfo { get; set; }
        public EssSiteInfo EssSiteInfo { get; set; }
        public RabbitMQ RabbitMQ { get; set; }
        public EssCLIInfo EssCLIInfo { get; set; }
    }

    public sealed class IdentityServiceInfo
    {
        public string SourceFolder {  get; set; }
        public string DBName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }

    public sealed class IdCLIServiceInfo
    {
        public string SourceFolder { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }

    public sealed class MessagingServiceInfo
    {
        public string SourceFolder { get; set; }
        public string DBName { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
        public Scheduler Scheduler { get; set; }
        public WebApi WebApi { get; set; }
    }
    public sealed class Scheduler
    {
        public string Host { get; set; }
        public string Port { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }
    public sealed class WebApi
    {
        public string Host { get; set; }
        public string Port { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }

    public sealed class StorageServiceInfo
    {
        public string Host { get; set; }
        public string SourceFolder { get; set; }
        public string Port { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
        public string FileStoragePath { get; set; }
    }

    public sealed class DocumentServiceInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string SourceFolder { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }

    public sealed class SignServiceInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string DBName { get; set; }
        public string SourceFolder { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }

    public sealed class EssServiceInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string DBName { get; set; }
        public string SourceFolder { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }

    public sealed class EssSiteInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string SourceFolder { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }

    public sealed class RabbitMQ
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }

    public sealed class EssCLIInfo
    {
        public string SourceFolder { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }
}
