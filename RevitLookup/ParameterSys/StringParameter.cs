using System.Reflection;

namespace RevitLookupWpf.ParameterSys
{
    public class StringParameter : ParameterBase<string>
    {
        public StringParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
        }
    }
}
