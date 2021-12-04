namespace RvtLookupWpf.PropertySys
{
    public class StringProperty : PropertyBase<string>
    {
        public StringProperty(string name, string value):base(name)
        {
            Value = value;

            IsClass = true;
        }
    }
}
