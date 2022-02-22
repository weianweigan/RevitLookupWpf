using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.CommandWpf;
using RevitLookupWpf.View;

namespace RevitLookupWpf.PropertySys.BaseProperty.ReferenceType
{
    public class DefaultObjectProperty : ObjectProperty<object>
    {
        private RelayCommand _selectedCommand;

        public DefaultObjectProperty(string name, string fullName, object value) : base(name, fullName)
        {


            if (value != null)
            {
                if (value is ElementId id) ResolveElementId(value, id);
                else if(value is XYZ xyz) ResolveXYZValue(value,xyz);
                else
                {
                    Value = value;
                    ValueType = value.GetType()?.Name;
                }

            }
        }

        /// <summary>
        /// User Click this object to Snoop
        /// </summary>
        //public event NaviRequest OnNaviRequest;

        public string ValueType { get; set; }

        public ICommand SelectedCommand => _selectedCommand ??= new RelayCommand(Selected);

        private void Selected()
        {
            if (SnoopOption.WindowOrNavi)
            {
                var lookupWindow = new LookupWindow();
                lookupWindow.SetRvtInstance(Value);
                lookupWindow.ShowDialog();
            }
            else
            {
                //使用面包屑导航
                NaviRvtObj(Value);
            }
        }

        void ResolveElementId(object value, ElementId id)
        {
            var element = SnoopingContext.Instance.CommandData.Application.ActiveUIDocument.Document
                .GetElement(id);
            if (element == null)
            {
                Value = value;
                ValueType = "<Null>";
            }
            else
            {
                Value = element;
                ValueType = $"<{element.GetType().Name} {element.Name} {element.Id.IntegerValue}>";
            }
        }

        void ResolveXYZValue(object value,XYZ xyz)
        {
            Value = value;
            ValueType = xyz.ToString();
        }
    }
}
