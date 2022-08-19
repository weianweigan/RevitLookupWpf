using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class FaceGeometryNode : GeometryNode<Face>
    {
        public FaceGeometryNode(Face rvtGeometryObject) 
            : base(rvtGeometryObject)
        {
            Name = $"{rvtGeometryObject.GetType().Name}({rvtGeometryObject.Area})";
        }

        public override Visual3D LoadModel3D()
        {
            var geo = RvtGeometryObject.ToGeometry3D();

            var geoModel3d = new GeometryModel3D()
            {
                Geometry = geo,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.LightBlue)),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.LightBlue)),
            };

            Visual3D = new ModelVisual3D()
            {
                Content = geoModel3d
            };

            return Visual3D;
        }
    }
}
