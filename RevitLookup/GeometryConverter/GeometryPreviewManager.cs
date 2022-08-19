using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryTree;
using RevitLookupWpf.InstanceTree;
using RevitLookupWpf.PropertySys;
using RevitLookupWpf.PropertySys.BaseProperty;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.View;
 
namespace RevitLookupWpf.GeometryConverter
{
    public static class GeometryPreviewManager
    {
        public static PreviewWindow PreviewWindow { get; private set; }

        internal static void Preview(PropertyBase selectedProperty)
        {
            if (PreviewWindow == null)
            {
                PreviewWindow = new PreviewWindow();
                PreviewWindow.Closed += PreviewWindow_Closed;
            }

            GeometryNode geometryNode = default;
            if (selectedProperty is DefaultObjectProperty defaultObject)
            {
                //Solid Line Curve
                if (defaultObject.Value is GeometryObject geometryObject)
                {
                    geometryNode = GeometryNode.CreateByGeometryObject(geometryObject);
                }
            }else if (selectedProperty is XYZProperty xYZProperty)
            {
                geometryNode = GeometryNode.CreateXYZ(xYZProperty.Value);
            }

            if (geometryNode == null)
            {
                return;
            }

            PreviewWindow.AddGeometryNode(geometryNode);
            PreviewWindow.Show();
            PreviewWindow.ThreeDView.ZoomExtents();
        }

        internal static void Preview(GeometryObjectInstanceNode geometryObjectInstanceNode)
        {
            if (PreviewWindow == null)
            {
                PreviewWindow = new PreviewWindow();
                PreviewWindow.Closed += PreviewWindow_Closed;
            }

            var geometryNode = GeometryNode.CreateByGeometryObject(geometryObjectInstanceNode.RvtObject);

            PreviewWindow.AddGeometryNode(geometryNode);
            PreviewWindow.Show();
            PreviewWindow.ThreeDView.ZoomExtents();
        }

        internal static void Preview(XYZInstanceNode xYZInstanceNode)
        {
            if (PreviewWindow == null)
            {
                PreviewWindow = new PreviewWindow();
                PreviewWindow.Closed += PreviewWindow_Closed;
            }

            var geometryNode = GeometryNode.CreateXYZ(xYZInstanceNode.RvtObject);

            PreviewWindow.AddGeometryNode(geometryNode);
            PreviewWindow.Show();
            PreviewWindow.ThreeDView.ZoomExtents();
        }

        internal static void Preview(BoundingBoxXYZInstanceNode boundingBoxInstanceNode)
        {
            if (PreviewWindow == null)
            {
                PreviewWindow = new PreviewWindow();
                PreviewWindow.Closed += PreviewWindow_Closed;
            }

            var geometryNode = GeometryNode.CreateBoundingBoxXYZ(boundingBoxInstanceNode.RvtObject);

            PreviewWindow.AddGeometryNode(geometryNode);
            PreviewWindow.Show();
            PreviewWindow.ThreeDView.ZoomExtents();
        }

        private static void PreviewWindow_Closed(object sender, EventArgs e)
        {
            PreviewWindow.Closed -= PreviewWindow_Closed;
            PreviewWindow = null;
        }
    }
}
