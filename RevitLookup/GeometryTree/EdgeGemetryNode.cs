using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class EdgeGemetryNode : GeometryNode<Edge>
    {
        public EdgeGemetryNode(Edge rvtGeometryObject) 
            : base(rvtGeometryObject)
        {
        }

        public override Visual3D LoadModel3D()
        {
            Visual3D = RvtGeometryObject
                .AsCurve()
                .ToCurveVisual3D();

            return Visual3D;
        }
    }
}
