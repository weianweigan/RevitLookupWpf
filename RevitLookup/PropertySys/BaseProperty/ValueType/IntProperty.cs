namespace RevitLookupWpf.PropertySys.BaseProperty.ValueType
{
    public class IntProperty : PropertyBase<int>
    {
        public IntProperty(string name, string fullName, int value) : base(name,fullName)
        {
            Value = value;
        }

        public int CompareTo(IntProperty other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
