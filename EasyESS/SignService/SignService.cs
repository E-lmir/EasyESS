﻿using CommandExecutor;
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

        public void CreateDb(InstallationInfo info)
        {
            //cd C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\
            var executor = new CommandLineExecutor();
            var dbScriptPath = Path.Combine(info.SignServiceInfo.SourceFolder, "DatabaseScripts\\SqlServer\\InitializeDatabase.sql");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -Q \"CREATE DATABASE {info.SignServiceInfo.DBName}\"");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.SignServiceInfo.DBName} -i {dbScriptPath}");
            executor.Execute(@$"cd {info.SQLCmdPath}", $"sqlcmd -U {info.DBServerUser} -P {info.DBServerPassword} -S {info.DBServerName} -d {info.SignServiceInfo.DBName} -i {dbScriptPath}");
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.SignServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<SignServiceConfig>(file);
            json.ConnectionStrings.IdentityService = $"Name=Directum.Core.IdentityService;Host={info.IdentityServiceInfo.Host};UseSsl=true;Port={info.IdentityServiceInfo.Port};User ID=DocServiceUser;Password=11111;";
            json.ConnectionStrings.StorageService = $"Name=Storage service;Host={info.StorageServiceInfo.Host};UseSsl=false;Port={info.StorageServiceInfo.Port};";
            json.ConnectionStrings.CloudSignService = "Name=CloudSigningService;Host=ca.foxtrot.comp.npo;Port=8007;UseSsl=false;";
            json.ConnectionStrings.CloudAuthService = "Name=CloudSigningService;Host=ca.foxtrot.comp.npo;Port=8007;UseSsl=false;";
            json.CryptoPro.OperatorLogin = "blazhnovvvdirectum";
            json.CryptoPro.OperatorPassword = "1Qwerty";
            json.CryptoPro.OperatorPassword = "1Qwerty";
            json.CryptoPro.HealthCheckUrl = "http://ca.foxtrot.comp.npo:8007/health";
            json.CloudSigningService.OperatorLogin = "Operator";
            json.CloudSigningService.OperatorPassword = "1Qwerty";
            json.CloudSigningService.Database = $"ProviderName=System.Data.SqlClient;Data Source={info.DBServerName};Initial Catalog={info.SignServiceInfo.DBName};Integrated Security=false;User ID={info.DBServerUser};Password={info.DBServerPassword};";
            json.CloudSigningService.DocumentServiceConnectionString = $"Name=Directum.Core.DocumentService;Host={info.DocumentServiceInfo.Host};UseSsl=false;Port={info.DocumentServiceInfo.Port};";
            json.CloudSigningService.MessageBrokerConnectionString = $"Name=Directum.Core.MessageBroker;Host={info.MessagingServiceInfo.WebApi.Host};Port={info.MessagingServiceInfo.WebApi.Port};UseSsl=false;";
            json.Authentication.TrustedIssuers[0].SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
        }

        public void IdCLIRegistration(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var audiencePath = Path.Combine(info.SignServiceInfo.SourceFolder, "SignServiceAudience.json");
            File.AppendAllText("C:/idCommands.txt", $"id add user \"SignServiceUser\" -p password=\"11111\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id add user \"SignServiceOperator\" -p password=\"11111\"" + Environment.NewLine);
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
            this.CreateDb(info);
        }
    }
}
