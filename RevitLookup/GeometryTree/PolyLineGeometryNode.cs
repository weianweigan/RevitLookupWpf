using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class PolyLineGeometryNode : GeometryNode<PolyLine>
    {
        public PolyLineGeometryNode(PolyLine rvtGeometryObject)
            : base(rvtGeometryObject)
        {
        }

        public override Visual3D LoadModel3D()
        {
            Visual3D = RvtGeometryObject.ToGeometry3D();

            return Visual3D;
        }
    }
}
