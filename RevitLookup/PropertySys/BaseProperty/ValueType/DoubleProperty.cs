namespace RevitLookupWpf.PropertySys.BaseProperty.ValueType
{
    public class DoubleProperty : PropertyBase<double>
    {
        public DoubleProperty(string name, string fullName,double value) : base(name,fullName)
        {
            Value = value;
        }
    }
}
