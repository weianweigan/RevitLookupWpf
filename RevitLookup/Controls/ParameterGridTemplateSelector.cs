using RevitLookupWpf.ParameterSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RevitLookupWpf.Controls
{
    public class ParameterGridTemplateSelector : DataTemplateSelector
    {
        #region Properties
        public DataTemplate IntDataTemplate { get; set; }

        public DataTemplate StringDataTemplate { get; set; }

        public DataTemplate BoolDataTemplate { get; set; }

        public DataTemplate DoubleDataTemplate { get; set; }

        public DataTemplate ViewDataTemplate { get; set; }

        public DataTemplate EnumDataTemplate { get; set; }

        public DataTemplate GuidDataTemplate { get; set; }
        #endregion

        #region Public Methods
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)return null;

            var type = item.GetType();
            DataTemplate dataTemplate = null;

            switch (type.Name)
            {
                case nameof(IntParameter):
                    dataTemplate = IntDataTemplate;
                    break;
                case nameof(StringParameter):
                    dataTemplate = StringDataTemplate;
                    break;
                case nameof(DoubleParameter):
                    dataTemplate = DoubleDataTemplate;
                    break;
                case nameof(BoolParameter):
                    dataTemplate = BoolDataTemplate;
                    break;
                case nameof(ViewParameter):
                    dataTemplate = ViewDataTemplate;
                    break;
                case nameof(EnumParameter):
                    dataTemplate = EnumDataTemplate;
                    break;
                case nameof(GuidParameter):
                    dataTemplate = GuidDataTemplate;
                    break;
                default:
                    //break;
                    throw new NotImplementedException(type.Name + "don't have a matched control template");
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
