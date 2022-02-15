using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.ParameterSys
{
    public class GuidParameter : ParameterBase<Guid>
    {
        private string _guidString;

        public GuidParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
        }

        public string GuidString
        {
            get => _guidString; set
            {
                _guidString = value;
                if(Guid.TryParse(value,out var id))
                {
                    Value = id;
                }    
            }
        }
    }
}
