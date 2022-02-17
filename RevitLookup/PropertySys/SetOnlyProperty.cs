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
    public class SetOnlyProperty : ParametersProperty
    {
        private readonly PropertyInfo _propertyInfo;

        public SetOnlyProperty(string name,object parent, PropertyInfo propertyInfo) : base(name,parent)
        {
            _propertyInfo = propertyInfo;

            Parameters = propertyInfo.SetMethod
                .GetParameters()
                .ToParameterSyss()
                .ToList();

            ValueType = $"Set({propertyInfo.SetMethod.GetParameters().AggregateParameters()})";
            ToolTip = ValueType;

            DetermainReturnInfo(null);
        }

        protected override object Invoke()
        {
            return _propertyInfo.SetMethod.Invoke(_parent, Parameters.Select(p => p.GetValue()).ToArray());
        }
    }
}
