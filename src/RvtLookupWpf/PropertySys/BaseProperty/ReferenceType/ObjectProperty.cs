namespace RvtLookupWpf.PropertySys
{
    public class ObjectProperty<TObject> : PropertyBase<TObject>
        where TObject : class
    {
        public ObjectProperty(string name) : base(name)
        {
            IsClass = true;
        }
    }
}
