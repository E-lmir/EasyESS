﻿using CommandExecutor;
using EasyESS.DocumentService;
using EasyESS.IdentityService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Document
{
    public class DocumentService
    {
        public void ExtractFiles(InstallationInfo info)
        {
            Directory.CreateDirectory(info.InstanceFolder);
            info.DocumentServiceInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "DocumentService")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.DocumentServiceInfo.SourceFolder, "WebApiService-win-x64.zip"), info.DocumentServiceInfo.ServiceFolder, true);
        }

        public void AddToIIS(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            executor.Execute($"cd c:\\Windows\\System32\\inetsrv", "appcmd add apppool /name:Document19 /managedRuntimeVersion: /managedPipelineMode:Integrated", $"appcmd add site /name:Document19 /physicalPath:{info.DocumentServiceInfo.ServiceFolder} /bindings:http/*:{info.DocumentServiceInfo.Port}:", "APPCMD.exe set app \"Document19/\" /applicationPool:\"Document19\"");
        }

        public void IdCLIRegistration(InstallationInfo info)
        {
            var executor = new CommandLineExecutor();
            var audiencePath = Path.Combine(info.DocumentServiceInfo.SourceFolder, "DocumentServiceAudience.json");
            File.AppendAllText("C:/idCommands.txt", $"id add user \"DocServiceUser\" - p password = \"11111\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id assign -u \"DocServiceUser\" -r \"service\"" + Environment.NewLine);
            File.AppendAllText("C:/idCommands.txt", $"id add resource \"Directum.Core.DocumentService\" -c \"{audiencePath}\"" + Environment.NewLine);
            executor.Execute($"cd {info.IdCLIServiceInfo.ServiceFolder}", $"id add user \"DocServiceUser\" - p password = \"11111\"", $"id assign -u \"DocServiceUser\" -r \"service\"", $"id add resource \"Directum.Core.DocumentService\" -c \"{audiencePath}\"");
        }

        public void FillConfig(InstallationInfo info)
        {
            var configPath = Path.Combine(info.DocumentServiceInfo.ServiceFolder, "appsettings.json");
            var file = File.ReadAllText(configPath);
            var json = JsonConvert.DeserializeObject<DocumentConfig>(file);
            json.ConnectionStrings.IdentityService = $"Name=Directum.Core.IdentityService;Host={info.IdentityServiceInfo.Host};UseSsl=true;Port={info.IdentityServiceInfo.Port};User ID=DocServiceUser;Password=11111;";
            json.ConnectionStrings.StorageService = $"Name=Storage service;Host={info.StorageServiceInfo.Host};UseSsl=false;Port={info.StorageServiceInfo.Port};";
            json.Authentication.SigningCertificateThumbprint = info.SigningCertificateThumbprint;
            file = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText(configPath, file);
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