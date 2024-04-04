using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.EssCLI
{
    public class EssCLIService
    {
        public void ExtractFiles(InstallationInfo info)
        {
            info.EssCLIInfo.ServiceFolder = Directory.CreateDirectory(Path.Combine(info.InstanceFolder, "EssCLI")).FullName;
            ZipFile.ExtractToDirectory(Path.Combine(info.EssCLIInfo.SourceFolder, "Ess-Cli-win-x64.zip"), info.EssCLIInfo.ServiceFolder, true);
        }

        public void Install(InstallationInfo info) => this.ExtractFiles(info);
    }
}
