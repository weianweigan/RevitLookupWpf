using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.Extension
{
    public static class MethodInfoExtension
    {
        public static string GetFullName(this MethodInfo methodInfo)
        {
            var parameterInfos = methodInfo.GetParameters();
            //var returnInfoStr = methodInfo.ReturnType?.FullName;
            
            if (parameterInfos == null || parameterInfos.Length == 0)
            {
                return $"{methodInfo.DeclaringType.FullName}.{methodInfo.Name}";
            }
            else
            {
                var parameterStr = parameterInfos.Length == 1 ?
                    parameterInfos[0].ParameterType.FullName :
                    parameterInfos
                        .Select(p => p.ParameterType.FullName)
                        .Aggregate((p1, p2) => $"{p1},{p2}");

                return $"{methodInfo.DeclaringType.FullName}.{methodInfo.Name}.{parameterStr}";
            }
        }

        public static string GetFullName(this PropertyInfo propertyInfo)
        {
            return $"{propertyInfo.DeclaringType.FullName}.{propertyInfo.Name}";

        }
    }
}
