using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class MeshGeometryNode : GeometryNode<Mesh>
    {
        public MeshGeometryNode(Mesh rvtGeometryObject) 
            : base(rvtGeometryObject)
        {
        }

        public override Visual3D LoadModel3D()
        {
            var meshGeo = RvtGeometryObject.ToGeometry3D();
            var geoModel3d = new GeometryModel3D()
            {
                Geometry = meshGeo,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Gray)),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray)),
            };

            Visual3D = new ModelVisual3D()
            {
                Content = geoModel3d
            };

            return Visual3D;
        }
    }
}
