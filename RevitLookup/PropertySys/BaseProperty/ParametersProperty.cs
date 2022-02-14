using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.PropertySys.BaseProperty
{
    /// <summary>
    /// A property which has getter index or set parameter and A method which has some value-type parameters
    /// </summary>
    public class ParametersProperty : PropertyBase<object>
    {
        protected readonly object _parent;

        public ParametersProperty(string name,object parent) : base(name)
        {
            _parent = parent;
        }
    }
}
