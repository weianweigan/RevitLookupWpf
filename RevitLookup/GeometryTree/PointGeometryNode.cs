using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class PointGeometryNode : GeometryNode<Point>
    {
        public PointGeometryNode(Point rvtGeometryObject) 
            : base(rvtGeometryObject)
        {
        }

        public override Visual3D LoadModel3D()
        {
            var points = new PointsVisual3D();
            points.Points.Add(ToPoint3D(RvtGeometryObject));
            Visual3D = points;

            return Visual3D;
        }

        private Point3D ToPoint3D(Point point)
        {
            return point.Coord.ToPoint3D();
        }
    }
}
