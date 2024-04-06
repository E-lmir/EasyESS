using CommandExecutor;
using EasyESS.Contracts;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;


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
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", 
                $"appcmd add apppool /name:EssSite{info.InstanceTag} /managedRuntimeVersion: /managedPipelineMode:Integrated", 
                $"appcmd add site /name:EssSite{info.InstanceTag} /physicalPath:{info.EssSiteInfo.ServiceFolder} /bindings:https/*:{info.EssSiteInfo.Port}:",
                $"APPCMD.exe set app \"EssSite{info.InstanceTag}/\" /applicationPool:\"EssSite{info.InstanceTag}\"");
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
            file = JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void Register(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var audiencePath = Path.Combine(info.EssServiceInfo.SourceFolder, "EssSiteAudience.json");
            executor.Execute($"{info.IdCLIServiceInfo.ServiceFolder.Substring(0, 2)}", 
                $"cd {info.IdCLIServiceInfo.ServiceFolder}",
                $"id add resource \"Directum.Core.EssSite\" -c \"{audiencePath}\" -p returnUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p originUrl=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}\" -p icon=\"https://{info.EssSiteInfo.Host}:{info.EssSiteInfo.Port}/logo_32.png\"");
        }

        public void FillWebConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.EssSiteInfo.ServiceFolder, "web.config");
            var xml = new XmlSerializer(typeof(EssSiteWebConfig));
            using var fs = new FileStream(configPath, FileMode.OpenOrCreate);
            var config = xml.Deserialize(fs) as EssSiteWebConfig;
            if (config != null)
            {
                config.systemwebServer = new configurationSystemwebServer();
                config.systemwebServer.rewrite = new configurationSystemwebServerRewrite();
                config.systemwebServer.rewrite.rules = new configurationSystemwebServerRewriteRule[2];
                config.systemwebServer.rewrite.rules[0] = new configurationSystemwebServerRewriteRule
                {
                    name = "storage",
                    stopProcessing = true,
                    action = new configurationSystemwebServerRewriteRuleAction
                    {
                        type = "Rewrite",
                        url = $"http://{info.StorageServiceInfo.Host}:{info.StorageServiceInfo.Port}/{{R:1}}"
                    },
                    match = new configurationSystemwebServerRewriteRuleMatch { url = "^storage/(.*)" }
                };

                config.systemwebServer.rewrite.rules[1] = new configurationSystemwebServerRewriteRule
                {
                    name = "api",
                    stopProcessing = true,
                    action = new configurationSystemwebServerRewriteRuleAction
                    {
                        type = "Rewrite",
                        url = $"https://{info.EssServiceInfo.Host}:{info.EssServiceInfo.Port}/api/{{R:1}}"
                    },
                    match = new configurationSystemwebServerRewriteRuleMatch { url = "^api/(.*)" }
                };
            }

            File.Delete(configPath);
            using var fileStream = new FileStream(configPath, FileMode.OpenOrCreate);
            xml.Serialize(fileStream, config);
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
