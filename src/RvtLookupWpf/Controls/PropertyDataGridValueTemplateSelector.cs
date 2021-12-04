/*
 * Created By WeiGan 2021.9.10
 * 属性模板选择器
 */

using System;
using System.Windows;
using System.Windows.Controls;
using RvtLookupWpf.PropertySys;

namespace RvtLookupWpf.Controls
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
                default:
                    throw new NotImplementedException(type.Name);
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
