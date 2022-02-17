namespace RevitLookupWpf.PropertySys.BaseProperty.ReferenceType
{
    public class StringProperty : PropertyBase<string>
    {
        public StringProperty(string name, string value):base(name)
        {
            Value = value.Replace("\n"," ").Replace("\r"," ");
            IsClass = true;
        }
    }
}
