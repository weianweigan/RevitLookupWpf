using System.Windows.Input;
using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Command;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.View;

namespace RevitLookupWpf.PropertySys
{
    public class ElementIdProperty : ObjectProperty<Element>
    {
        private RelayCommand _selectedCommand;

        public ElementIdProperty(string name, string fullName, ElementId value)
            : base(name, fullName)
        {
            if (value != null)
            {
                var element = SnoopingContext.Instance.CommandData.Application.ActiveUIDocument.Document
                .GetElement(value);
                if (element == null)
                {
                    Value = element;
                    ValueType = "<Null>";
                }
                else
                {
                    Value = element;
                    ValueType = $"<{element.GetType().Name} {element.Name} {element.Id.IntegerValue}>";
                }
            }
        }

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
    }
}
