using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.ParameterSys
{
    public class ViewParameter : ParameterBase<Autodesk.Revit.DB.View>
    {
        public ViewParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
            Value = SnoopingContext.Instance.CommandData.View;
            Views = new List<Autodesk.Revit.DB.View>() { Value };
        }

        public List<Autodesk.Revit.DB.View> Views { get;protected set; }

    }
}
