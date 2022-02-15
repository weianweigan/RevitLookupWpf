using RevitLookupWpf.ParameterSys;
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
            return parameter.ParameterType.IsValueTypeOrString() || AllowTypes.Contains(parameter.ParameterType);
        }

        public static bool IsAllInputable(this ParameterInfo[] parameters,bool containEmpty = false)
        {
            if (parameters== null || parameters.Length == 0)
                return containEmpty;

            return parameters.All(p => IsInputable(p));
        }

        public static string AggregateParameters(this ParameterInfo[] parameterInfos)
        {
            if (parameterInfos?.Any() != true)
            {
                return string.Empty;
            }

            return parameterInfos.Length == 1 ?
                $"{parameterInfos[0].ParameterType.Name} {parameterInfos[0].Name}" :
                parameterInfos
                    .Select(p => $"{p.ParameterType.Name} {p.Name}")
                    .Aggregate((p1, p2) => $"{p1},{p2}");
        }

        public static IEnumerable<ParameterBase> ToParameterSyss(this ParameterInfo[] parameterInfos)
        {
            foreach (var parameterInfo in parameterInfos)
            {
                yield return ToParameterSys(parameterInfo);
            }
        }

        private static ParameterBase ToParameterSys(ParameterInfo parameterInfo)
        {
            ParameterBase parameter;

            if (parameterInfo.ParameterType.IsEnum)
            {
                return new EnumParameter(parameterInfo);
            }

            switch (parameterInfo.ParameterType.FullName)
            {
                case "System.String":
                    parameter = new StringParameter(parameterInfo);
                    break;
                case "System.Int32":
                    parameter = new IntParameter(parameterInfo);
                    break;
                case "System.Boolean":
                    parameter = new BoolParameter(parameterInfo);
                    break;
                case "System.Double":
                    parameter = new DoubleParameter(parameterInfo);
                    break;
                case "System.Guid":
                    parameter = new GuidParameter(parameterInfo);
                    break;
                case "Autodesk.Revit.DB.View":
                    parameter = new ViewParameter(parameterInfo);
                    break;
                default:
                    //parameter = new StringParameter(parameterInfo);
                    //break;
                    throw new InvalidOperationException("Not matched parameter type ");
            }
            
            return parameter;
        }
    }
}
