using System.Reflection;

namespace RevitLookupWpf.ParameterSys
{
    public class IntParameter : ParameterBase<int>
    {
        public IntParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
        }
    }
}
