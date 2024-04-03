using CommandExecutor;
using EasyESS.IdentityService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.IdCLI
{
    public class IdCLIService
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

        public void AddRoles(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            File.AppendAllText("C:/idCommands.txt", $"id add role \"service\"");
            executor.Execute($"cd {info.IdCLIServiceInfo.ServiceFolder}", $"id add role \"service\"");
        }

        public void Install(InstallationInfo info)
        {
            this.ExtractFiles(info);
            this.FillConfig(info);
            this.AddRoles(info);
        }
    }
}
