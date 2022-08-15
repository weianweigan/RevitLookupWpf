using Autodesk.Revit.DB;
using CommunityToolkit.Mvvm.Input;

namespace RevitLookupWpf.InstanceTree
{
    public class GeometryObjectInstanceNode : InstanceNode<GeometryObject>
    {
        private RelayCommand _previewCommand;

        public GeometryObjectInstanceNode(GeometryObject rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
#if R19 || R20
                Name += $"({rvtObject.GetHashCode()})";
#else
                Name += $"({rvtObject.Id})";
#endif

            }
        }

        public override bool IsGeometryObject => true;

        public RelayCommand PreviewCommand { get => _previewCommand ??= new RelayCommand(PreviewClick); }

        private void PreviewClick()
        {
            if (RvtObject is Solid solid)
            {
                GeometryConverter.GeometryPreviewManager.Preview(solid);
            }
        }
    }
}
