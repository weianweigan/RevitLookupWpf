namespace RevitLookupWpf.PropertySys.BaseProperty.ValueType
{
    public class EnumProperty : PropertyBase<string>
    {
        public EnumProperty(string name, string fullName, Enum enumValue) : base(name, fullName)
        {
            Value = enumValue.ToString();
        }
    }
}
