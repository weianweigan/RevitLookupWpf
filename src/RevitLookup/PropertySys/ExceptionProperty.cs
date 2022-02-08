using RevitLookup.PropertySys.BaseProperty.ReferenceType;

namespace RevitLookup.PropertySys
{
    public class ExceptionProperty:ObjectProperty<Exception>
    {
        public ExceptionProperty(string name,Exception exception):base(name)
        {
            Value = exception;
            Msg = Value.Message;
        }

        public string Msg { get; set; }
    }
}
