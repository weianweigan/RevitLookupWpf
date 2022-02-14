using RevitLookupWpf.PropertySys.BaseProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.PropertySys
{
    public class InputableMethodProperty : ParametersProperty
    {
        private readonly MethodInfo _methodInfo;

        public InputableMethodProperty(string name, object parent,MethodInfo methodInfo) : base(name, parent)
        {
            _methodInfo = methodInfo;
        }
    }
}
