using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Contracts
{
    public interface IDataAccessable
    {
        public void CreateDb(InstallationInfo info);
    }
}
