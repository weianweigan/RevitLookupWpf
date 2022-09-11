using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Command;
using Microsoft.Windows.Input;

namespace RevitLookupWpf.InstanceTree
{
    public class XYZInstanceNode : InstanceNode<XYZ>
    {
        private RelayCommand _previewCommand;

        public XYZInstanceNode(XYZ rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject})";
            }
        }

        public override bool IsGeometryObject => true;

        public RelayCommand PreviewCommand { get => _previewCommand ??= new RelayCommand(PreviewClick); }

        private void PreviewClick()
        {
            GeometryConverter.GeometryPreviewManager.Preview(this);
        }
    }
}
