using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Command;

namespace RevitLookupWpf.InstanceTree
{
    public class BoundingBoxXYZInstanceNode : InstanceNode<BoundingBoxXYZ>
    {
        private RelayCommand _previewCommand;

        public BoundingBoxXYZInstanceNode(BoundingBoxXYZ rvtObject) 
            : base(rvtObject)
        {
        }

        public override bool IsGeometryObject => true;

        public RelayCommand PreviewCommand { get => _previewCommand ??= new RelayCommand(PreviewClick); }

        private void PreviewClick()
        {
            GeometryConverter.GeometryPreviewManager.Preview(this);
        }
    }
}
