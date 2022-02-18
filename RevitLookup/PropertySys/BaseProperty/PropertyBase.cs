using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.View;
using System.Windows.Input;

namespace RevitLookupWpf.PropertySys.BaseProperty
{
    public abstract class PropertyBase:ObservableObject
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Category { get; set; }

        public bool IsClass { get;protected set; }

        public bool IsReadOnly { get;set; }

        public bool IsMethod { get; set; }

        public string ToolTip { get; set; }

        public string APIName => IsMethod ? $"M:{FullName}" : $"P:{FullName}";

        public RevitInfo GetRevitInfo()
        {
            return RevitInfoManager.Find(APIName);
        }
    }

    public abstract class PropertyBase<T> : PropertyBase
    {
        protected PropertyBase(string name,string fullName)
        {
            Name = name;
            FullName = fullName;
        }

        public T Value { get; set; }
    }
}
