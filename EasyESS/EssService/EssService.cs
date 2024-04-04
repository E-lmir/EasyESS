using CommandExecutor;
using EasyESS.DocumentService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.EssService
{
    public class EssService
    {
        public void CreateDb(InstallationInfo info)
        {
            //cd C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\
            var executor = new CommandLineExecutor();
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -Q \"CREATE DATABASE {info.EssServiceInfo.DBName}\"");
            var dbScriptPath = Path.Combine(info.EssServiceInfo.SourceFolder, "DatabaseScripts\\SqlServer\\InitializeDatabase.sql");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.EssServiceInfo.DBName} -i {dbScriptPath}");
        }

        public void IdCLIRegistration(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var audiencePath = Path.Combine(info.EssServiceInfo.SourceFolder, "EssServiceAudience.json");
            File.AppendAllText("C:/idCommands.txt", $"id add user \"EssServiceUser\" -p password=\"11111\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id assign -u \"EssServiceUser\" -r \"service\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id add resource \"Directum.Core.EssService\" -c \"{audiencePath}\" -p icon=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}/logo_32.png\"" + Environment.NewLine);
            //TODO Add missing commands
           // executor.Execute($"cd {info.IdCLIServiceInfo.ServiceFolder}", $"id add user \"DocServiceUser\" - p password = \"11111\"", $"id assign -u \"SignServiceUser\" -r \"service\"", $"id add resource \"Directum.Core.SignService\" -c \"{audiencePath}\"");
        }

        public void ExtractFiles(InstallationInfo info)
        {
            info.EssServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "EssService")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.EssServiceInfo.SourceFolder, "RxIntegrationClient-win-x64.zip"), info.EssServiceInfo.ServiceFolder, true);
            ZipFile.ExtractToDirectory(Path.Combine(info.EssServiceInfo.SourceFolder, "OfficeService-win-x64.zip"), info.EssServiceInfo.ServiceFolder, true);
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.EssServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<EssServiceConfig>(file);
            json.ConnectionStrings.IdentityService = $"Name=Directum.Core.IdentityService;Host={info.IdentityServiceInfo.Host};UseSsl=true;Port={info.IdentityServiceInfo.Port};User ID=EssServiceUser;Password=11111;";
            json.ConnectionStrings.StorageService = $"Name=Storage service;Host={info.StorageServiceInfo.Host};UseSsl=false;Port={info.StorageServiceInfo.Port};";
            json.ConnectionStrings.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.EssServiceInfo.DBName};Integrated Security=False;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            json.ConnectionStrings.RabbitMQ = $"userName={info.RabbitMQ.User};password={info.RabbitMQ.Password};hostName={info.RabbitMQ.Host};port={info.RabbitMQ.Port};virtualHost={info.RabbitMQ.VirtualHost};";
            json.ConnectionStrings.SignService = $"Name=Directum.Core.SignService;Host={info.SignServiceInfo.Host};UseSsl=true;Port={info.SignServiceInfo.Port};";
            json.ConnectionStrings.MessagingService = $"Name=Directum.Core.MessageBroker;Host={info.MessagingServiceInfo.WebApi.Host};UseSsl=false;Port={info.MessagingServiceInfo.WebApi.Port};";
            json.ConnectionStrings.DocumentService = $"Name=Directum.Core.DocumentService;Host={info.DocumentServiceInfo.Host};UseSsl=false;Port={info.DocumentServiceInfo.Port};";
            json.Authentication.SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", "appcmd add apppool /name:EssService19 /managedRuntimeVersion: /managedPipelineMode:Integrated", $"appcmd add site /name:EssService19 /physicalPath:{info.EssServiceInfo.ServiceFolder} /bindings:http/*:{info.EssServiceInfo.Port}:", "APPCMD.exe set app \"EssService19/\" /applicationPool:\"EssService19\"");
        }

        public void Install(InstallationInfo info)
        {
            this.CreateDb(info);
            this.IdCLIRegistration(info);
            this.ExtractFiles(info);
            this.FillConfig(info);
            this.AddToIIS(info);
        }
    }
}
