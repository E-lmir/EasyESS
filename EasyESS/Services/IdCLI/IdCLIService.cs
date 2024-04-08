﻿using CommandExecutor;
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
            executor.Execute($"{info.IdCLIServiceInfo.ServiceFolder.Substring(0, 2)}", 
                $"cd {info.IdCLIServiceInfo.ServiceFolder}", 
                $"id add role \"service\"");
        }
    }
}
