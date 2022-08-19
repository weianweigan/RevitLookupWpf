using GalaSoft.MvvmLight;
using RevitLookupWpf.PropertySys.BaseProperty;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.PropertySys.BaseProperty.ValueType;

namespace RevitLookupWpf.ViewModel
{
    public class UnitConverterViewModel : ObservableObject
    {
        private string _result;

        public string Result
        {
            get => _result; set
            {
                Set(ref _result, value);
            }
        }

        public void Update(PropertyBase property)
        {
            if (property is DoubleProperty)
            {

            }else if(property is StringProperty)
            {

            }
        }
    }
}
