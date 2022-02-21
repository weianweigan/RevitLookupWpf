using Autodesk.Revit.DB;
using RevitLookupWpf.Extension;
using RevitLookupWpf.PropertySys.BaseProperty;
using System.Reflection;

namespace RevitLookupWpf.PropertySys
{
    public class SetOnlyNameStringProperty : PropertyBase<string>
    {
        private static string[] _names = new string[] { nameof(Element), nameof(ElementType) };

        public SetOnlyNameStringProperty(string name, object rvtObject, PropertyInfo propertyInfo):base(name,propertyInfo.GetFullName())
        {
            if (rvtObject is Element ele)
            {
                Value = ele.Name;
            }else if (rvtObject is ElementType eleType)
            {
                Value = eleType.Name;
            }
        }

        public static bool IsSpecialType(Type type)
        {
            var baseType = type;

            while (baseType != null)
            {
                if (_names.Contains(baseType.Name))                
                    return true;
                
                baseType = baseType.BaseType;
            }

            return false;
        }
    }
}
