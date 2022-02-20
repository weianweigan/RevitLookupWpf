using Autodesk.Revit.DB;
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
        private List<Autodesk.Revit.DB.View> _view;

        public ViewParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
            
        }

        public List<Autodesk.Revit.DB.View> AllViews
        {
            get
            {
                if (_view == null)
                    _view = new List<Element>(new FilteredElementCollector(SnoopingContext.Instance.CommandData.Application.ActiveUIDocument.Document)
                            .OfClass(typeof(Autodesk.Revit.DB.View))
                            .WhereElementIsNotElementType()
                            .ToList()
                            .OrderBy(v => v.Name))
                            .Distinct()
                            .ToList()
                            .Cast<Autodesk.Revit.DB.View>()
                            .ToList()
                            .Where(x => !x.Name.Contains($"<Revision Schedule>"))
                            .Where(x => !x.IsTemplate)
                            .ToList();

                return _view;
            }
            set
            {
                _view = value;
            }
        }

    }
}
