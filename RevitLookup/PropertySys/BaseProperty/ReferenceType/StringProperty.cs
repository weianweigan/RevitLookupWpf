namespace RevitLookupWpf.PropertySys.BaseProperty.ReferenceType
{
    public class StringProperty : PropertyBase<string>
    {
        public StringProperty(string name,string fullName ,string value):base(name,fullName)
        {
            Value = value.Replace("\n"," ").Replace("\r"," ");
            IsClass = true;
        }
    }
}
