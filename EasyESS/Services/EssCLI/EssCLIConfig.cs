using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.Services.EssCLI
{
    public class EssCLIConfig
    {
        public Connectionstrings ConnectionStrings { get; set; }
    }

    public class Connectionstrings
    {
        public string Database { get; set; }
    }

}
