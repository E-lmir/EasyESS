using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS
{
    public sealed class InstallationInfo
    {
        public string InstanceFolder { get; set; }
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
        public string Host { get; set; }
        public string Port { get; set; }
        [JsonIgnore]
        public string ServiceFolder { get; set; }
    }
}
