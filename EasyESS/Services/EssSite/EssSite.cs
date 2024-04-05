using CommandExecutor;
using EasyESS.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Services.EssSite
{
    public class EssSite : IService, IRegistrable, IHostable
    {
        public void ExtractFiles(InstallationInfo info)
        {
            info.EssSiteInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "EssSite")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.EssSiteInfo.SourceFolder, "Site-win-x64.zip"), info.EssSiteInfo.ServiceFolder, true);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", "appcmd add apppool /name:EssSite19 /managedRuntimeVersion: /managedPipelineMode:Integrated", $"appcmd add site /name:EssSite19 /physicalPath:{info.EssSiteInfo.ServiceFolder} /bindings:https/*:{info.EssSiteInfo.Port}:", "APPCMD.exe set app \"EssSite19/\" /applicationPool:\"EssSite19\"");
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.EssSiteInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<EssSiteConfig>(file);
            json.ConnectionStrings.IdentityService = $"Name=Directum.Core.IdentityService;Host={info.IdentityServiceInfo.Host};UseSsl=true;Port={info.IdentityServiceInfo.Port};User ID=EssServiceUser;Password=11111;";
            json.ConnectionStrings.OfficeService = $"Name=Directum.Core.EssService;Host={info.EssServiceInfo.Host};UseSsl=true;Port={info.EssServiceInfo.Port};User ID=EssServiceUser;Password=11111;";
            json.Authentication.ReturnUrl = $"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}";
            json.Authentication.SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            json.ReverseProxy.Routes.StorageService.Transforms = null;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void Register(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var audiencePath = Path.Combine(info.EssServiceInfo.SourceFolder, "EssSiteAudience.json");
            File.AppendAllText("C:/idCommands.txt", $"id add resource \"Directum.Core.EssSite\" -c \"{audiencePath}\" -p returnUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p originUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p icon=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}/logo_32.png\"" + Environment.NewLine);
            //TODO Add missing commands
            executor.Execute($"{info.IdCLIServiceInfo.ServiceFolder.Substring(0, 2)}", 
                $"cd {info.IdCLIServiceInfo.ServiceFolder}",
                $"id add resource \"Directum.Core.EssSite\" -c \"{audiencePath}\" -p returnUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p originUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p icon=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}/logo_32.png\"");
        }

        public void Install(InstallationInfo info)
        {
            ExtractFiles(info);
            FillConfig(info);
            Register(info);
            AddToIIS(info);
        }
    }
}
