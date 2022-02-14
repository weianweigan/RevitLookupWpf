using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.Helpers
{
    /// <summary>
    /// To determine weather a paraemter can be choose or input
    /// </summary>
    public static class ParametersUtil
    {
        public static List<Type> AllowTypes { get; } = new List<Type>()
        {
            typeof(Autodesk.Revit.DB.View),
        };

        public static bool IsInputable(this ParameterInfo parameter)
        {
            return parameter.ParameterType.IsValueType || AllowTypes.Contains(parameter.ParameterType);
        }

        public static bool IsAllInputable(this ParameterInfo[] parameters)
        {
            if (parameters == null)
                return false;

            return parameters.All(p => IsInputable(p));
        }
    }
}
