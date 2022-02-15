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
    public class GetIndexerProperty : ParametersProperty
    {
        private readonly PropertyInfo _propertyInfo;

        public GetIndexerProperty(string name, object parent,PropertyInfo propertyInfo) : base(name, parent)
        {
            Parameters = propertyInfo.GetMethod
                .GetParameters()
                .ToParameterSyss()
                .ToList();
            _propertyInfo = propertyInfo;

            DetermainReturnInfo(propertyInfo.GetMethod.ReturnType);

            ValueType = ReturnTypeName + $" Get({propertyInfo.GetMethod.GetParameters().AggregateParameters()})";
            ToolTip = ValueType;
        }

        protected override object Invoke()
        {
            return _propertyInfo.GetMethod.Invoke(_parent,Parameters.Select(p => p.GetValue()).ToArray());
        }
    }
}
