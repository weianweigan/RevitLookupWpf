using Autodesk.Revit.UI;
using RevitLookupWpf.Extension;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.PropertySys.BaseProperty;
using System.Reflection;

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
            try
            {
                return _methodInfo.Invoke(_parent, Parameters.Select(p => p.GetValue()).ToArray());
            }
            catch (Exception ex)
            {
                TaskDialog.Show(ex.GetType().Name, ex.ToString());
            }

            return null;
        }
    }
}
