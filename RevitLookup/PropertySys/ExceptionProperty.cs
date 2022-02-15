using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;

namespace RevitLookupWpf.PropertySys
{
    public class ExceptionProperty:ObjectProperty<Exception>
    {
        public ExceptionProperty(string name,Exception exception):base(name)
        {
            Value = exception;
            Msg = Value.Message;

            ToolTip = Value.ToString();
        }

        public string Msg { get; set; }
    }
}
