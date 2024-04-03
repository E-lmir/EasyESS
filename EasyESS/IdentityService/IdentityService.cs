using CommandExecutor;
using Newtonsoft.Json;
using System.IO.Compression;

namespace EasyESS.IdentityService
{
    public class IdentityService
    {
        public static void ExtractFiles(InstallationInfo info)
        {
            Directory.CreateDirectory(info.InstanceFolder);
            info.IdentityServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "IdentityService")).Name;
            ZipFile.ExtractToDirectory(Path.Combine(info.IdentityServiceInfo.SourceFolder, "IdentityService-win-x64.zip"), info.IdentityServiceInfo.ServiceFolder, true);
            ZipFile.ExtractToDirectory(Path.Combine(info.IdentityServiceInfo.SourceFolder, "EssPlatformIdentityProvider2-win-x64.zip"), info.IdentityServiceInfo.ServiceFolder, true);
        }

        public static void CreateDb(InstallationInfo info)
        {
            //cd C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\
            var executor = new CommandLineExecutor();
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -Q \"CREATE DATABASE {info.IdentityServiceInfo.DBName}\"");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.IdentityServiceInfo.DBName} -i {Path.Combine(info.IdentityServiceInfo.SourceFolder, "DatabaseScripts\\SqlServer\\InitializeDatabase.sql")}");
        }

        public static void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.IdentityServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<IdentityConfig>(file);
            json.ConnectionStrings.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.IdentityServiceInfo.DBName};Integrated Security=False;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            json.ConnectionStrings.MessagingService = $"Name=Directum.Core.MessageBroker;Host={info.MessagingServiceInfo.Host};UseSsl=false;Port={info.MessagingServiceInfo.Port};";
            json.ConnectionStrings.ShortPathService = "";
            json.TokenIssuer.SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            json.General.ServiceEndpoint = $"https://{info.IdentityServiceInfo.Host}:{info.IdentityServiceInfo.Port}";
            json.AccountEnrichment.Providers[0].Configuration.EssPlatformVersion = info.EssPlatformVersion;
            json.AccountEnrichment.Providers[0].Configuration.IntegrationServiceEndpoint = info.IntegrationServiceEndpoint;
            json.AccountEnrichment.Providers[0].Configuration.ServiceUsername = info.IntegrationServiceUser;
            json.AccountEnrichment.Providers[0].Configuration.ServiceUserPassword = info.IntegrationServicePassword;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public static void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", "appcmd add apppool /name:Identity /managedRuntimeVersion: /managedPipelineMode:Integrated", $"appcmd add site /name:Identity /physicalPath:{info.IdentityServiceInfo.ServiceFolder} /bindings:https/{info.IdentityServiceInfo.Host}:{info.IdentityServiceInfo.Port}:", "APPCMD.exe set app \"Identity/\" /applicationPool:\"Identity\"");
        }

        public static void Install()
        {
            var info = new InstallationInfo();
            info.IdentityServiceInfo = new IdentityServiceInfo();
            info.IdentityServiceInfo.Port = "666";
            var res = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText("D:\\IdsTest\\1.json", res);
        }
    }
}
