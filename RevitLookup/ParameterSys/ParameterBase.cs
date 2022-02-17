using System.Reflection;

namespace RevitLookupWpf.ParameterSys
{
    public abstract class ParameterBase
    {
        protected ParameterBase(ParameterInfo parameterInfo)
        {
            ParameterInfo = parameterInfo;
            Name = parameterInfo.Name;
        }

        public string Name { get; }

        public ParameterInfo ParameterInfo { get; }

        public abstract object GetValue();
    }

    public abstract class ParameterBase<TValue> : ParameterBase
    {
        protected ParameterBase(ParameterInfo parameterInfo) 
            : base(parameterInfo)
        {
        }

        public TValue Value { get; set; }

        public override object GetValue() => Value;
    }
}
