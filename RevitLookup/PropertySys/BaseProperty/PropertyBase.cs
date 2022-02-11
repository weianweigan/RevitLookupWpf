using GalaSoft.MvvmLight.CommandWpf;
using RevitLookupWpf.View;
using System.Windows.Input;

namespace RevitLookupWpf.PropertySys.BaseProperty
{
    public abstract class PropertyBase
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public bool IsClass { get;protected set; }

        public bool IsReadOnly { get;set; }

        public bool IsMethod { get; set; }

        public string ToolTip { get; set; }
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
