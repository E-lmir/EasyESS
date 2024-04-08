using CommandExecutor;
using EasyESS.Contracts;
using Newtonsoft.Json;
using System.IO.Compression;

namespace EasyESS.Services.SignService
{
    public class SignService : IService, IDataAccessable, IHostable
    {
        public void ExtractFiles(InstallationInfo info)
        {
            Directory.CreateDirectory(info.InstanceFolder);
            info.SignServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "SignService")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.SignServiceInfo.SourceFolder, "Directum.Core.SignService.App-win-x64.zip"), info.SignServiceInfo.ServiceFolder, true);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", 
                $"appcmd add apppool /name:SignService{info.InstanceTag} /managedRuntimeVersion: /managedPipelineMode:Integrated", 
                $"appcmd add site /name:SignService{info.InstanceTag} /physicalPath:{info.SignServiceInfo.ServiceFolder} /bindings:https/*:{info.SignServiceInfo.Port}:", 
                $"APPCMD.exe set app \"SignService{info.InstanceTag}/\" /applicationPool:\"SignService{info.InstanceTag}\"");
        }

        public void CreateDb(InstallationInfo info)
        {
            //cd C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\
            var executor = new CommandLineExecutor();
            var dbScriptPath = Path.Combine(info.SignServiceInfo.SourceFolder, "DatabaseScripts\\SqlServer\\InitializeDatabase.sql");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -Q \"CREATE DATABASE {info.SignServiceInfo.DBName}\"");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.SignServiceInfo.DBName} -i {dbScriptPath}");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.SignServiceInfo.DBName} -i {dbScriptPath}");
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.SignServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<SignServiceConfig>(file);
            json.ConnectionStrings.IdentityService = $"Name=Directum.Core.IdentityService;Host={info.IdentityServiceInfo.Host};UseSsl=true;Port={info.IdentityServiceInfo.Port};User ID=DocServiceUser;Password=11111;";
            json.ConnectionStrings.StorageService = $"Name=Storage service;Host={info.StorageServiceInfo.Host};UseSsl=false;Port={info.StorageServiceInfo.Port};";
            json.ConnectionStrings.CloudSignService = "Name=CloudSigningService;Host=ca.foxtrot.comp.npo;Port=8107;UseSsl=false;";
            json.ConnectionStrings.CloudAuthService = "Name=CloudSigningService;Host=ca.foxtrot.comp.npo;Port=8107;UseSsl=false;";
            json.CryptoPro.HealthCheckUrl = "http://ca.foxtrot.comp.npo:8107/health";
            json.CryptoPro.OperatorLogin = "blazhnovvvdirectum";
            json.CryptoPro.OperatorPassword = "1Qwerty";
            json.CryptoPro.HealthCheckUrl = "http://ca.foxtrot.comp.npo:8007/health";
            json.CloudSigningService.OperatorLogin = "SignServiceOperator";
            json.CloudSigningService.OperatorPassword = "11111";
            json.CloudSigningService.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.SignServiceInfo.DBName};Integrated Security=false;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            json.CloudSigningService.DocumentServiceConnectionString = $"Name=Directum.Core.DocumentService;Host={info.DocumentServiceInfo.Host};UseSsl=false;Port={info.DocumentServiceInfo.Port};";
            json.CloudSigningService.MessageBrokerConnectionString = $"Name=Directum.Core.MessageBroker;Host={info.MessagingServiceInfo.WebApi.Host};Port={info.MessagingServiceInfo.WebApi.Port};UseSsl=false;";
            json.Authentication.TrustedIssuers[0].SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }
    }
}
