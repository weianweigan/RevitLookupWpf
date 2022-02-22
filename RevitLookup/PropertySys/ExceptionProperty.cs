using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;

namespace RevitLookupWpf.PropertySys
{
    public class ExceptionProperty:ObjectProperty<Exception>
    {
        public ExceptionProperty(string name, string fullName, Exception exception):base(name,fullName)
        {
            Value = exception.InnerException ?? exception;
            Msg = Value.Message;

            ToolTip = Value.GetType().FullName;
        }

        public string Msg { get; set; }
    }
}
