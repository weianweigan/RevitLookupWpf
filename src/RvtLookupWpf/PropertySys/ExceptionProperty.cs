using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvtLookupWpf.PropertySys
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
