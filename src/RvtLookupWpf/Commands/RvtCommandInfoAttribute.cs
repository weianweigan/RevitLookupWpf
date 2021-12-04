using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvtLookupWpf.Commands
{
    public class RvtCommandInfoAttribute : Attribute
    {
        public string Name { get; set; }

        public string Description {  get; set; }

        public string Image { get;set; }
    }
}
