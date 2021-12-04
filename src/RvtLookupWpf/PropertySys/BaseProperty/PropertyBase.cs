using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvtLookupWpf.PropertySys
{
    public abstract class PropertyBase
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public bool IsClass { get;protected set; }

        public bool IsReadOnly { get;set; }

        public bool IsMethod { get; set; }
    }

    public abstract class PropertyBase<T> : PropertyBase
    {
        protected PropertyBase(string name)
        {
            Name = name;
        }

        public T Value { get; set; }
    }
}
