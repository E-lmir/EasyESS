using EasyESS.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Services.EssCLI
{
    public class EssCLIService : IService
    {
        public void ExtractFiles(InstallationInfo info)
        {
            info.EssCLIInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "EssCLI")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.EssCLIInfo.SourceFolder, "Ess-Cli-win-x64.zip"), info.EssCLIInfo.ServiceFolder, true);
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.EssCLIInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<EssCLIConfig>(file);
            json.ConnectionStrings.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.EssServiceInfo.DBName};Integrated Security=False;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void Install(InstallationInfo info)
        {
            ExtractFiles(info);
            FillConfig(info);

        }
    }
}
