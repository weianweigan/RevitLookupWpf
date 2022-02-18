namespace RevitLookupWpf.PropertySys.BaseProperty.ValueType
{
    public class BooleanProperty : PropertyBase<bool>
    {
        public BooleanProperty(string name, string fullName,bool value) : base(name,fullName)
        {
            Value = value;
        }
    }
}
