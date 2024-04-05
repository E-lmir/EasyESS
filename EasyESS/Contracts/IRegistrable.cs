using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Contracts
{
    public interface IRegistrable
    {
        public void Register(InstallationInfo info);
    }
}
