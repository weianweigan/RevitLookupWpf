using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using RevitLookup.View;

namespace RevitLookup.PropertySys.BaseProperty.ReferenceType
{
    public class DefaultObjectProperty : ObjectProperty<object>
    {
        private RelayCommand _selectedCommand;

        public DefaultObjectProperty(string name,object value) : base(name)
        {
            Value = value;

            if (value != null)
            {
                ValueType = value.GetType()?.Name;
            }
        }

        /// <summary>
        /// User Click this object to Snoop
        /// </summary>
        //public event NaviRequest OnNaviRequest;

        public string ValueType { get; set; }

        public ICommand SelectedCommand => _selectedCommand ?? (_selectedCommand = new RelayCommand(Selected));

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
