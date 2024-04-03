using CommandExecutor;
using EasyESS.IdentityService;
using EasyESS.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.StorageService
{
    public class StorageService
    {
        public void ExtractFiles(InstallationInfo info)
        {
            Directory.CreateDirectory(info.InstanceFolder);
            info.StorageServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "StorageService")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.StorageServiceInfo.SourceFolder, "StorageApi.Service-win-x64.zip"), info.StorageServiceInfo.ServiceFolder, true);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", "appcmd add apppool /name:Storage19 /managedRuntimeVersion: /managedPipelineMode:Integrated", $"appcmd add site /name:Storage19 /physicalPath:{info.IdentityServiceInfo.ServiceFolder} /bindings:http/*:{info.StorageServiceInfo.Port}:", "APPCMD.exe set app \"Storage19/\" /applicationPool:\"Storage19\"");
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.StorageServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<StorageConfig>(file);
            var path = Path.Combine(info.StorageServiceInfo.ServiceFolder, "storage");
            Directory.CreateDirectory(path);
            json.General.FileStoragePath = path;
            json.Authentication.SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void IdCLIRegistration(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd {info.IdCLIServiceInfo.ServiceFolder}", $"id add resource \"Directum.Core.BlobStorageService\" -c \"{info.StorageServiceInfo.SourceFolder}\\BlobStorageServiceAudience.json\"");
        }

        public void Install(InstallationInfo info)
        {
            this.ExtractFiles(info);
            this.AddToIIS(info);
            this.FillConfig(info);
            this.IdCLIRegistration(info);
        }
    }
}
