using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.ParameterSys
{
    public class EnumParameter : ParameterBase<int>
    {
        private string _selectedItem;

        public EnumParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
            var values = Enum.GetNames(parameterInfo.ParameterType);
            foreach (var value in values)
            {
                EnumValues.Add(value);
            }
            SelectedItem = values.FirstOrDefault();
        }


        public List<string> EnumValues { get; set; } = new List<string>();

        public string SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;

                Value = (int)Enum.Parse(ParameterInfo.ParameterType,_selectedItem);
            }
        }
    }
}
