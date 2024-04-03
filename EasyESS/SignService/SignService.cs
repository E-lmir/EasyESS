using CommandExecutor;
using EasyESS.DocumentService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.SignService
{
    public class SignService
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
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", "appcmd add apppool /name:SignService19 /managedRuntimeVersion: /managedPipelineMode:Integrated", $"appcmd add site /name:SignService19 /physicalPath:{info.SignServiceInfo.ServiceFolder} /bindings:http/*:{info.SignServiceInfo.Port}:", "APPCMD.exe set app \"SignService19/\" /applicationPool:\"SignService19\"");
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.SignServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<SignServiceConfig>(file);
            json.ConnectionStrings.IdentityService = $"Name=Directum.Core.IdentityService;Host={info.IdentityServiceInfo.Host};UseSsl=true;Port={info.IdentityServiceInfo.Port};User ID=DocServiceUser;Password=11111;";
            json.ConnectionStrings.StorageService = $"Name=Storage service;Host={info.StorageServiceInfo.Host};UseSsl=false;Port={info.StorageServiceInfo.Port};";
            json.ConnectionStrings.CloudSignService = "Name=CryptoProSignService;Host=ca.foxtrot.comp.npo;Port=8007;UseSsl=false;";
            json.ConnectionStrings.CloudAuthService = "Name=CryptoProAuthService;Host=ca.foxtrot.comp.npo;Port=8007;UseSsl=false;";
            json.CryptoPro.OperatorLogin = "blazhnovvvdirectum";
            json.CryptoPro.OperatorPassword = "1Qwerty";
            json.CryptoPro.OperatorPassword = "1Qwerty";
            json.CryptoPro.HealthCheckUrl = "http://ca.foxtrot.comp.npo:8007/health";
            json.Authentication.TrustedIssuers[0].SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void IdCLIRegistration(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var audiencePath = Path.Combine(info.SignServiceInfo.SourceFolder, "SignServiceAudience.json");
            File.AppendAllText("C:/idCommands.txt", $"id add user \"SignServiceUser\" - p password = \"11111\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id add user \"SignServiceOperator\" - p password = \"11111\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id assign -u \"SignServiceUser\" -r \"service\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id add role \"Admins\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id assign -u \"SignServiceOperator\" -r \"Admins\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id add role \"Users\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id add resource \"Directum.Core.SignService\" -c \"{audiencePath}\"" + Environment.NewLine);
            //TODO Add missing commands
            executor.Execute($"cd {info.IdCLIServiceInfo.ServiceFolder}", $"id add user \"DocServiceUser\" - p password = \"11111\"", $"id assign -u \"SignServiceUser\" -r \"service\"", $"id add resource \"Directum.Core.SignService\" -c \"{audiencePath}\"");
        }

        public void Install(InstallationInfo info)
        {
            this.ExtractFiles(info);
            this.FillConfig(info);
            this.AddToIIS(info);
            this.IdCLIRegistration(info);
        }
    }
}
