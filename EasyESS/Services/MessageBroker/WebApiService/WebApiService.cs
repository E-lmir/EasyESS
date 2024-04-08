using CommandExecutor;
using EasyESS.Contracts;
using EasyESS.Services.IdentityService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Services.MessageBroker.WebApiService
{
    public class WebApiService : IService, IDataAccessable, IHostable
    {
        public void CreateDb(InstallationInfo info)
        {
            //cd C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\
            var executor = new CommandLineExecutor();
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -Q \"CREATE DATABASE {info.MessagingServiceInfo.DBName}\"");
            var dbScriptPath = Path.Combine(info.MessagingServiceInfo.SourceFolder, "DatabaseScripts\\SqlServer\\InitializeDatabase.sql");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.MessagingServiceInfo.DBName} -i {dbScriptPath}");
        }

        public void ExtractFiles(InstallationInfo info)
        {
            info.MessagingServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "CoreMessageBroker")).FullName;
            info.MessagingServiceInfo.WebApi.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.MessagingServiceInfo.ServiceFolder, "WebApi")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.MessagingServiceInfo.SourceFolder, "WebApiService-win-x64.zip"), info.MessagingServiceInfo.WebApi.ServiceFolder, true);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", 
                $"appcmd add apppool /name:CoreMessageBroker{info.InstanceTag} /managedRuntimeVersion: /managedPipelineMode:Integrated", 
                $"appcmd add site /name:CoreMessageBroker{info.InstanceTag} /physicalPath:{info.MessagingServiceInfo.WebApi.ServiceFolder} /bindings:http/*:{info.MessagingServiceInfo.WebApi.Port}:",
                $"APPCMD.exe set app \"CoreMessageBroker{info.InstanceTag}/\" /applicationPool:\"CoreMessageBroker{info.InstanceTag}\"");
        }
        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.MessagingServiceInfo.WebApi.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<WebApiConfig>(file);
            json.ConnectionStrings.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.MessagingServiceInfo.DBName};Integrated Security=False;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            json.Authentication.SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            json.Scheduler.HealthCheckUrl = $"http://{info.MessagingServiceInfo.Scheduler.Host}:{info.MessagingServiceInfo.Scheduler.Port}/health";
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }
    }
}
