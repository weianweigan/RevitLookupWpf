/*
 * Created By WeiGan 2021.9.10
 * 属性模板选择器
 */

using System.Windows;
using System.Windows.Controls;
using RevitLookupWpf.PropertySys;
using RevitLookupWpf.PropertySys.BaseProperty.MethodType;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.PropertySys.BaseProperty.ValueType;

namespace RevitLookupWpf.Controls
{
    public class PropertyDataGridValueTemplateSelector : DataTemplateSelector
    {
        #region Properties
        public DataTemplate IntDataTemplate { get; set; }

        public DataTemplate StringDataTemplate { get; set; }

        public DataTemplate BoolDataTemplate { get; set; }

        public DataTemplate DoubleDataTemplate {  get; set;}

        public DataTemplate ExceptionDataTemplate { get; set; }

        public DataTemplate MethodDataTemplate {  get; set; }

        public DataTemplate DefaultObjectDataTemplate {  get; set; }

        public DataTemplate NullObjectDataTemplate { get; set; }

        public DataTemplate ParameterDataTemplate { get; set; }
        #endregion

        #region Public Methods
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
                //throw new ArgumentNullException("item");
            }

            var type = item.GetType();
            DataTemplate dataTemplate;

            switch (type.Name)
            {
                case nameof(IntProperty):
                    dataTemplate = IntDataTemplate;
                    break;
                case nameof(StringProperty):
                case nameof(EnumProperty):
                case nameof(GuidProperty):
                case nameof(SetOnlyNameStringProperty):
                    dataTemplate = StringDataTemplate;
                    break;
                case nameof(DoubleProperty):
                    dataTemplate = DoubleDataTemplate;
                    break;
                case nameof(BooleanProperty):
                    dataTemplate = BoolDataTemplate;
                    break;
                case nameof(MethodProperty):
                    dataTemplate = MethodDataTemplate;
                    break;
                case nameof(ExceptionProperty):
                    dataTemplate = ExceptionDataTemplate;
                    break;
                case nameof(DefaultObjectProperty):
                    dataTemplate = DefaultObjectDataTemplate;
                    break;
                case nameof(NullObjectProperty):
                        dataTemplate = NullObjectDataTemplate;
                    break;
                case nameof(GetIndexerProperty):
                case nameof(InputableMethodProperty):
                case nameof(SetOnlyProperty):
                    dataTemplate = ParameterDataTemplate;
                    break;
                default:
                    //dataTemplate = DefaultObjectDataTemplate;
                    //break;
                    throw new NotImplementedException(type.Name+"don't have a matched control template");
            }

            if (dataTemplate == null)
            {
                throw new NotImplementedException(type.Name);
            }

            return dataTemplate;
        }
        #endregion
    }
}
