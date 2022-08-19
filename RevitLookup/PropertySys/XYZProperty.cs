using System.Windows.Input;
using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Command;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.View;

namespace RevitLookupWpf.PropertySys
{
    public class XYZProperty : ObjectProperty<XYZ>
    {
        private RelayCommand _selectedCommand;

        public XYZProperty(string name, string fullName,XYZ value) : base(name, fullName)
        {
            if (value != null)
            {
                Value = value;
                ValueType = value.ToString();
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
