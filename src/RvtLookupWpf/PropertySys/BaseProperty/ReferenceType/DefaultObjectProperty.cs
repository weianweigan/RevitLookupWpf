using RvtLookupWpf.View;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace RvtLookupWpf.PropertySys
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

        public string ValueType { get; set; }

        public ICommand SelectedCommand => _selectedCommand ?? (_selectedCommand = new RelayCommand(Selected));

        private void Selected()
        {
            var lookupWindow = new LookupWindow();
            lookupWindow.SetRvtInstance(Value);
            lookupWindow.ShowDialog();
        }
    }
}
