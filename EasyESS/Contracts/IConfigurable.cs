using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Contracts
{
    internal interface IConfigurable
    {
        internal void Configure(InstallationInfo info);
    }
}
