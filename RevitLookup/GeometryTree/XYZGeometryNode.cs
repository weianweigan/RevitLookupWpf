using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class XYZGeometryNode : GeometryNode<XYZ>
    {
        public XYZGeometryNode(XYZ rvtGeometryObject) : base(rvtGeometryObject)
        {
            Name = $"{typeof(XYZ).Name}({rvtGeometryObject.ToPoint3D()})";
        }

        public override Visual3D LoadModel3D()
        {
            var points = new PointsVisual3D();
            points.Points.Add(RvtGeometryObject.ToPoint3D());
            Visual3D = points;

            return Visual3D;
        }
    }
}
