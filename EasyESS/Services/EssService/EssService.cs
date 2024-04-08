using CommandExecutor;
using EasyESS.Contracts;
using Newtonsoft.Json;
using System.IO.Compression;

namespace EasyESS.Services.EssService
{
    public class EssService : IService, IDataAccessable, IHostable
    {
        public void CreateDb(InstallationInfo info)
        {
            //cd C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\
            var executor = new CommandLineExecutor();
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -Q \"CREATE DATABASE {info.EssServiceInfo.DBName}\"");
            var dbScriptPath = Path.Combine(info.EssServiceInfo.SourceFolder, "DatabaseScripts\\SqlServer\\InitializeDatabase.sql");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.EssServiceInfo.DBName} -i {dbScriptPath}");
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
            json.ConnectionStrings.StorageService = $"Name=Directum.Core.BlobStorageService;Host={info.StorageServiceInfo.Host};UseSsl=false;Port={info.StorageServiceInfo.Port};";
            json.ConnectionStrings.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.EssServiceInfo.DBName};Integrated Security=False;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            json.ConnectionStrings.RabbitMQ = $"userName={info.RabbitMQ.User};password={info.RabbitMQ.Password};hostName={info.RabbitMQ.Host};port={info.RabbitMQ.Port};virtualHost={info.RabbitMQ.VirtualHost};";
            json.ConnectionStrings.SignService = $"Name=Directum.Core.SignService;Host={info.SignServiceInfo.Host};UseSsl=true;Port={info.SignServiceInfo.Port};";
            json.ConnectionStrings.MessagingService = $"Name=Directum.Core.MessageBroker;Host={info.MessagingServiceInfo.WebApi.Host};UseSsl=false;Port={info.MessagingServiceInfo.WebApi.Port};";
            json.ConnectionStrings.DocumentService = $"Name=Directum.Core.DocumentService;Host={info.DocumentServiceInfo.Host};UseSsl=false;Port={info.DocumentServiceInfo.Port};";
            json.ConnectionStrings.PreviewStorage = "";
            json.ConnectionStrings.PreviewService = "";
            json.Authentication.SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv",
                $"appcmd add apppool /name:EssService{info.InstanceTag} /managedRuntimeVersion: /managedPipelineMode:Integrated", 
                $"appcmd add site /name:EssService{info.InstanceTag} /physicalPath:{info.EssServiceInfo.ServiceFolder} /bindings:https/*:{info.EssServiceInfo.Port}:", 
                $"APPCMD.exe set app \"EssService{info.InstanceTag}/\" /applicationPool:\"EssService{info.InstanceTag}\"");
        }
    }
}
