using CommandExecutor;
using EasyESS.Contracts;
using Newtonsoft.Json;
using System.IO.Compression;

namespace EasyESS.Services.IdCLI
{
    public class IdCLIService : IService, IRegistrable
    {
        public void ExtractFiles(InstallationInfo info)
        {
            info.IdCLIServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "IdCLI")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.IdCLIServiceInfo.SourceFolder, "Id-Cli-win-x64.zip"), info.IdCLIServiceInfo.ServiceFolder, true);
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.IdCLIServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<IdCLIConfig>(file);
            json.ConnectionStrings.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.IdentityServiceInfo.DBName};Integrated Security=False;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void Register(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var docServiceAudiencePath = Path.Combine(info.DocumentServiceInfo.SourceFolder, "DocumentServiceAudience.json");
            var essServiceAudiencePath = Path.Combine(info.EssServiceInfo.SourceFolder, "EssServiceAudience.json");
            var essSiteAudiencePath = Path.Combine(info.EssServiceInfo.SourceFolder, "EssSiteAudience.json");
            var webApiAudiencePath = Path.Combine(info.MessagingServiceInfo.SourceFolder, "MessageBrokerAudience.json");
            var signServiceAudiencePath = Path.Combine(info.SignServiceInfo.SourceFolder, "SignServiceAudience.json");
            var storageServiceAudiencePath = Path.Combine(info.StorageServiceInfo.SourceFolder, "BlobStorageServiceAudience.json");
            executor.Execute($"{info.IdCLIServiceInfo.ServiceFolder.Substring(0, 2)}", 
                $"cd {info.IdCLIServiceInfo.ServiceFolder}", 
                $"id add role \"service\"",
                $"id add user \"DocServiceUser\" -p password=\"11111\"",
                $"id assign -u \"DocServiceUser\" -r \"service\"",
                $"id add resource \"Directum.Core.DocumentService\" -c \"{docServiceAudiencePath}\"",
                $"id add user \"EssServiceUser\" -p password=\"11111\"",
                $"id assign -u \"EssServiceUser\" -r \"service\"",
                $"id add resource \"Directum.Core.EssService\" -c \"{essServiceAudiencePath}\" -p icon=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}/logo_32.png\"",
                $"id add resource \"Directum.Core.EssSite\" -c \"{essSiteAudiencePath}\" -p returnUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p originUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p icon=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}/logo_32.png\"",
                $"id add resource \"Directum.Core.MessageBroker\" -c \"{webApiAudiencePath}\"",
                $"id add user \"SignServiceUser\" -p password=\"11111\"",
                $"id add user \"SignServiceOperator\" -p password=\"11111\"",
                $"id add role \"Admins\"",
                $"id add role \"Users\"",
                $"id assign -u \"SignServiceUser\" -r \"service\"",
                $"id assign -u \"SignServiceOperator\" -r \"Admins\"",
                $"id add resource \"Directum.Core.SignService\" -c \"{signServiceAudiencePath}\"",
                $"id add resource \"Directum.Core.BlobStorageService\" -c \"{storageServiceAudiencePath}\"");
        }
    }
}
