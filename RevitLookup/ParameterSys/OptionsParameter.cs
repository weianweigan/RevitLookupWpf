using Autodesk.Revit.DB;
using System.Reflection;

namespace RevitLookupWpf.ParameterSys
{
    public class OptionsParameter : ParameterBase<Options>
    {
        private List<Autodesk.Revit.DB.View> _view;

        public OptionsParameter(ParameterInfo parameterInfo) : base(parameterInfo)
        {
        }

        public List<Autodesk.Revit.DB.View> AllViews
        {
            get
            {
                if (_view == null)
                    _view = new List<Element>(new FilteredElementCollector(SnoopingContext.Instance.CommandData
                                .Application
                                .ActiveUIDocument.Document).OfClass(typeof(Autodesk.Revit.DB.View))
                            .WhereElementIsNotElementType().OrderBy(v => v.Name)).Distinct()
                        .Cast<Autodesk.Revit.DB.View>()
                        .Where(x => !x.Name.Contains($"<Revision Schedule>"))
                        .Where(x => !x.IsTemplate)
                        .ToList();

                return _view;
            }
            set => _view = value;
        }

        public Autodesk.Revit.DB.View SelectedView { get; set; }

        public bool ComputeReferences { get; set; }

        public List<ViewDetailLevel> ViewDetailLevels =>
            Enum.GetValues(typeof(ViewDetailLevel))
            .OfType<ViewDetailLevel>()
            .ToList();

        public ViewDetailLevel? DetailLevel { get; set; }

        public bool IncludeNonVisibleObjects { get; set; }

        public override object GetValue()
        {
            var options = new Options()
            {
                ComputeReferences = ComputeReferences,
                IncludeNonVisibleObjects = IncludeNonVisibleObjects
            };
            if (DetailLevel != null)
            {
                options.DetailLevel = DetailLevel.Value;
            }
            else
            {
                if (SelectedView != null)
                {
                    options.View = SelectedView;
                }
            }

            return options;
        }
    }
}