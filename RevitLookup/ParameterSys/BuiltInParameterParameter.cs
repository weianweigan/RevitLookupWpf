using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Diagnostics;
using System.Reflection;
using GalaSoft.MvvmLight.Command;

namespace RevitLookupWpf.ParameterSys
{
    /// <summary>
    /// Parameter class for user to input <see cref="BuiltInParameter"/>
    /// </summary>
    public class BuiltInParameterParameter : ParameterBase<BuiltInParameter>
    {
        private string _selectedItem;

        public BuiltInParameterParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
            if (_enumValues == null)
            {
                _enumValues = typeof(BuiltInParameter).GetEnumNames();
            }
        }

        public static string[] _enumValues { get; private set; }


        public string[] EnumValues => _enumValues;

        public string SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;

                if (Enum.TryParse(value,out BuiltInParameter builtInParameter))
                {
                    Value = builtInParameter;
                }
            }
        }

        public RelayCommand OpenUrlCommand { get; set; } = 
            new RelayCommand(() => Process.Start("https://www.revitapidocs.com/2022/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm"));

        public override bool IsLegal()
        {
            bool result = false;
            if (Enum.TryParse(_selectedItem, out BuiltInParameter builtInParameter))
            {
                result = true;
            }
            else
            {
                TaskDialog.Show("Error",$"Please input a legal parameter about BuiltInParameter");
            }

            return result;
        }
    }
}
