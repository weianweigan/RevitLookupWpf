namespace RevitLookupWpf.PropertySys.BaseProperty.ValueType
{
    public class IntProperty : PropertyBase<int>
    {
        public IntProperty(string name, int value) : base(name)
        {
            Value = value;
        }

        public int CompareTo(IntProperty other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
