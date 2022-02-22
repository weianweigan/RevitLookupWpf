/*
 * Created By WeiGan 2021.9.10
 * Extension Methods of RvtObject to Snoop Propeties and Methods
 */

using System.Reflection;
using Autodesk.Revit.UI;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.PropertySys;
using RevitLookupWpf.PropertySys.BaseProperty;
using RevitLookupWpf.PropertySys.BaseProperty.MethodType;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.PropertySys.BaseProperty.ValueType;

namespace RevitLookupWpf.Extension
{
    public static class ObjectExtension
    {
        #region Public Static Mehtod
        public static PropertyList GetProperties(this object rvtObject)
        {
            if (rvtObject is null)
            {
                throw new ArgumentNullException(nameof(rvtObject));
            }

            //init
            var list = new PropertyList(rvtObject);
            var type = rvtObject.GetType();

            list.Inheri = GetBaseChain(type);

            //properties
            var propertyInfos = type.GetProperties();

            foreach (var propertyInfo in propertyInfos.OrderBy(p => p.Name))
            {
                var property = default(PropertyBase);
                try
                {
                    property = GetProperty(propertyInfo, rvtObject,type);
                    property.IsReadOnly = propertyInfo.CanWrite;
                }
                catch (Exception ex)
                {
                    property = new ExceptionProperty(propertyInfo.Name,$"{type.FullName}.{propertyInfo.Name}",ex);
                }
                if (property != null)
                {
                    property.Category = "Properties";
                    list.Add(property);
                }
            }

            //Methods
            var methodInfos = type.GetMethods()
                .Where(p => !p.Name.StartsWith("get_") && !p.Name.StartsWith("set_"))
                .Where(m => !m.Name.StartsWith("add_") && !m.Name.StartsWith("remove_"))
                .Where(xyz=>!xyz.Name.StartsWith("op_"))
                .Where(p => p.Name != "Dispose")
                .ToList();
            //.OrderBy(p => p.Name);

            var methodPropeties = new PropertyBase[methodInfos.Count];

            for (int i = 0; i < methodInfos.Count; i++)
            {
                PropertyBase property;
                try
                {
                    if (methodInfos[i].GetParameters().IsAllInputable())
                    {
                        property = new InputableMethodProperty(methodInfos[i].Name, rvtObject, methodInfos[i]);
                    }
                    else
                    {
                        //Default Method Properties
                        property = new MethodProperty(methodInfos[i].Name,methodInfos[i], rvtObject);
                    }
                }
                catch (Exception ex)
                {
                    property = new ExceptionProperty(methodInfos[i].Name,methodInfos[i].GetFullName() ,ex)
                    {
                        IsMethod = true
                    };
                }
                if (property != null)
                {
                    property.Category = "Methods";
                    methodPropeties[i] = property;
                }
            };

            list.AddRange(methodPropeties);
            //list.Sort((p1,p2) => p1.Name.CompareTo(p2.Name));

            return list;
        }
        #endregion

        #region Private Static Mehtods
        private static string GetBaseChain(Type type)
        {
            var name = type.Name;
            var baseType = type.BaseType;

            while (baseType != null)
            {
                name += $"->{baseType.Name}";
                baseType = baseType.BaseType;
            }

            return name;
        }

        private static PropertyBase GetProperty(PropertyInfo propertyInfo, object rvtObject, Type type)
        {
            var property = default(PropertyBase);

            if (propertyInfo.GetMethod != null)
            {
                var parameters = propertyInfo.GetMethod.GetParameters();
                if (parameters.IsAllInputable())
                {
                    property = new GetIndexerProperty(propertyInfo.Name, rvtObject, propertyInfo);
                }else if (parameters?.Any() == true)
                {
                    throw new ArgumentException($"Parameter({parameters.AggregateParameters()}) cannot Input");
                }
                else
                {
                    property = GetNormalProperty(propertyInfo, rvtObject);
                }
            }
            else
            {
                if (propertyInfo.Name == "Name" && SetOnlyNameStringProperty.IsSpecialType(type))
                {
                    property = new SetOnlyNameStringProperty(propertyInfo.Name, rvtObject, propertyInfo);
                }
                else
                {
                    //Only Set
                    property = new SetOnlyProperty(propertyInfo.Name, rvtObject, propertyInfo);
                }
            }

            return property;
        }

        private static PropertyBase GetNormalProperty(PropertyInfo propertyInfo, object element)
        {
            PropertyBase property;

            var value = propertyInfo.GetValue(element);

            if (value == null)
            {
                property = new NullObjectProperty(propertyInfo.Name, propertyInfo.GetFullName());
            }
            else if (propertyInfo.PropertyType.IsClass)
            {
                switch (propertyInfo.PropertyType.FullName)
                {
                    case "System.String":
                        property = new StringProperty(propertyInfo.Name,propertyInfo.GetFullName() ,value as string);
                        break;

                    default:
                        if (value == null)
                        {
                            property = new NullObjectProperty(propertyInfo.Name, propertyInfo.GetFullName());
                        }
                        else
                        {
                            property = new DefaultObjectProperty(propertyInfo.Name, propertyInfo.GetFullName(), value);
                        }
                        break;
                }
            }
            else if(propertyInfo.PropertyType.IsEnum)
            {
                property = new EnumProperty(propertyInfo.Name, propertyInfo.GetFullName(),value as Enum);
            }
            else
            {
                //值类型
                switch (propertyInfo.PropertyType.FullName)
                {
                    case "System.Int32":
                        property = new IntProperty(propertyInfo.Name, propertyInfo.GetFullName(), (int)value);
                        break;
                    case "System.Boolean":
                        property = new BooleanProperty(propertyInfo.Name, propertyInfo.GetFullName(), (bool)value);
                        break;
                    case "System.Double":
                        property = new DoubleProperty(propertyInfo.Name, propertyInfo.GetFullName(), (double)value);
                        break;
                    case "System.Guid":
                        property = new GuidProperty(propertyInfo.Name, propertyInfo.GetFullName(), (Guid)value);
                        break;
                    default:
                        throw new NotSupportedException(propertyInfo.PropertyType.FullName);
                }
            }

            return property;
        }
        #endregion
    }
}
