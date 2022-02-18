using RevitLookupWpf.Extension;
using RevitLookupWpf.Helpers;
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

        public InputableMethodProperty(string name, object parent,MethodInfo methodInfo) : base(methodInfo.Name, methodInfo.GetFullName(),parent)
        {
            _methodInfo = methodInfo;
            IsMethod = true;

            ToolTip = $"{methodInfo.ReturnType?.Name} {name}({methodInfo.GetParameters().AggregateParameters()})";

            Parameters = methodInfo
                .GetParameters()
                .ToParameterSyss()
                .ToList();

            ValueType = ToolTip;

            DetermainReturnInfo(methodInfo.ReturnType);
        }

        protected override object Invoke()
        {
            return _methodInfo.Invoke(_parent, Parameters.Select(p => p.GetValue()).ToArray());
        }
    }
}
