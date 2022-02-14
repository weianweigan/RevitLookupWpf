using RevitLookupWpf.PropertySys.BaseProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.PropertySys
{
    public class SetOnlyProperty : ParametersProperty
    {
        public SetOnlyProperty(string name,object parent, PropertyInfo property) : base(name,parent)
        {
        }
    }
}
