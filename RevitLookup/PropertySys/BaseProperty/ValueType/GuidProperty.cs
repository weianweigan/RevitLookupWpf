namespace RevitLookupWpf.PropertySys.BaseProperty.ValueType
{
    public class GuidProperty : PropertyBase<Guid>
    {
        public GuidProperty(string name, string fullName,Guid guid) : base(name, fullName)
        {
            Value = guid;
        }
    }
}
