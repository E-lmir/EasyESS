using CommandExecutor;
using EasyESS.Contracts;
using Newtonsoft.Json;
using System.IO.Compression;

namespace EasyESS.Services.EssCLI
{
    public class EssCLIService : IService, IRegistrable
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

        public void Register(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var hrTargetSystemsPath = Path.Combine(info.HRRepositoryPath, "data\\AdapterConfig\\HRSolution_AdapterConfig.json");
            executor.Execute($"{info.EssCLIInfo.ServiceFolder.Substring(0, 2)}",
                $"cd {info.EssCLIInfo.ServiceFolder}",
                $"ess connect \"{info.EssBaseTargetSystemsPath}\" -p UserIdentity=\"DirectumRX\" -p Configuration:AppServerConnection:Endpoint=\"{info.IntegrationServiceEndpoint}\" -p Configuration:AppServerConnection:UserName=\"{info.IntegrationServiceUser}\" -p Configuration:AppServerConnection:Password=\"{info.IntegrationServicePassword}\" -p Configuration:ServerVersion=\"{info.RxVersion}\"",
                $"ess connect \"{hrTargetSystemsPath}\" -p UserIdentity=\"DirectumRX\" -p Configuration:AppServerConnection:Endpoint=\"{info.IntegrationServiceEndpoint}\" -p Configuration:AppServerConnection:UserName=\"{info.IntegrationServiceUser}\" -p Configuration:AppServerConnection:Password=\"{info.IntegrationServicePassword}\" -p Configuration:ServerVersion=\"{info.RxVersion}\"",
                $"ess install {info.EssBaseConfigurationPath} -a",
                $"ess install {Path.Combine(info.ESSRepositoryPath, "data\\EssConfig\\Roles.xml")} -a",
                $"ess install {Path.Combine(info.ESSRepositoryPath, "data\\EssConfig\\SignPlatform.xml")} -a",
                $"ess install {Path.Combine(info.ESSRepositoryPath, "data\\EssConfig\\UsageAgreements.xml")} -a",
                $"ess install {Path.Combine(info.HRRepositoryPath, "data\\EssConfig\\Statements.xml")} -a",
                $"ess install {Path.Combine(info.HRRepositoryPath, "data\\EssConfig\\StatementTilesExtension.xml")} -a",
                $"ess install {Path.Combine(info.HRRepositoryPath, "data\\EssConfig\\Vacations.xml")} -a");
        }
    }
}
