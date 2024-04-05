using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Contracts
{
    public interface IService
    {
        public void ExtractFiles(InstallationInfo info);
        public void Install(InstallationInfo info);
        public void FillConfig(InstallationInfo info);
    }
}
