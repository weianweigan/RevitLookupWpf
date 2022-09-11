using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class CurveGeometryNode : GeometryNode<Curve>
    {
        public CurveGeometryNode(Curve rvtGeometryObject)
            : base(rvtGeometryObject)
        {
            Name = $"{rvtGeometryObject.GetType().Name}({rvtGeometryObject.ApproximateLength})";
        }

        public override Visual3D LoadModel3D()
        {
            Visual3D = RvtGeometryObject.ToCurveVisual3D();
            return Visual3D;
        }
    }
}
