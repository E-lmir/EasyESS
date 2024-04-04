using CommandExecutor;
using EasyESS.MessageBroker.WebApiService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.MessageBroker.Scheduler
{
    public class SchedulerService
    {
        public void ExtractFiles(InstallationInfo info)
        {
            info.MessagingServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "CoreMessageBroker")).FullName;
            info.MessagingServiceInfo.Scheduler.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.MessagingServiceInfo.ServiceFolder, "Scheduler")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.MessagingServiceInfo.SourceFolder, "MtsMarketologTransport-win-x64.zip"), info.MessagingServiceInfo.Scheduler.ServiceFolder, true);
            ZipFile.ExtractToDirectory(Path.Combine(info.MessagingServiceInfo.SourceFolder, "MobilGroupTransport-win-x64.zip"), info.MessagingServiceInfo.Scheduler.ServiceFolder, true);
            ZipFile.ExtractToDirectory(Path.Combine(info.MessagingServiceInfo.SourceFolder, "SmtpTransport-win-x64.zip"), info.MessagingServiceInfo.Scheduler.ServiceFolder, true);
            ZipFile.ExtractToDirectory(Path.Combine(info.MessagingServiceInfo.SourceFolder, "SmscTransport-win-x64.zip"), info.MessagingServiceInfo.Scheduler.ServiceFolder, true);
            ZipFile.ExtractToDirectory(Path.Combine(info.MessagingServiceInfo.SourceFolder, "Scheduler-win-x64.zip"), info.MessagingServiceInfo.Scheduler.ServiceFolder, true);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", "appcmd add apppool /name:Scheduler19 /managedRuntimeVersion: /managedPipelineMode:Integrated", $"appcmd add site /name:Scheduler19 /physicalPath:{info.MessagingServiceInfo.Scheduler.ServiceFolder} /bindings:http/*:{info.MessagingServiceInfo.Scheduler.Port}:", "APPCMD.exe set app \"Scheduler19/\" /applicationPool:\"Scheduler19\"");
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.MessagingServiceInfo.Scheduler.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<SchedulerConfig>(file);
            json.ConnectionStrings.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.MessagingServiceInfo.DBName};Integrated Security=False;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            json.Transport.SmtpDeliveryProxy = "SMTP";
            json.Transport.ViberDeliveryProxy = "Viber";
            json.Transport.Proxies[0].Configuration.Username = "DIDSMS";
            json.Transport.Proxies[0].Configuration.Password = "7863400";
            json.Transport.Proxies[0].Configuration.Sender = "DIDSMS";
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void Install(InstallationInfo info)
        {
            this.ExtractFiles(info);
            this.AddToIIS(info);
            this.FillConfig(info);
        }
    }
}
