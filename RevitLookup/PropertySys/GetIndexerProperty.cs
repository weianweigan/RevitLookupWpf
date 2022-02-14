using RevitLookupWpf.PropertySys.BaseProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.PropertySys
{
    public class GetIndexerProperty : ParametersProperty
    {
        public GetIndexerProperty(string name, object parent,PropertyInfo parameterInfo) : base(name, parent)
        {

        }
    }
}
