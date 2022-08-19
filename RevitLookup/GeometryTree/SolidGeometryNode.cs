using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class SolidGeometryNode : GeometryNode<Solid>
    {
        public SolidGeometryNode(Solid rvtGeometryObject)
            : base(rvtGeometryObject)
        {
            Name = $"{typeof(Solid).Name}({rvtGeometryObject.Volume})";
        }

        public bool DisplayWire { get; set; } = true;

        public override Visual3D LoadModel3D()
        {
            var meshGeo = RvtGeometryObject.ToGeometry3D();
            var geoModel3d = new GeometryModel3D()
            {
                Geometry = meshGeo,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Gray)),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray)),
            };

            var modelVisual3D = new ModelVisual3D()
            {
                Content = geoModel3d
            };

            if (DisplayWire)
            {
                var wires = RvtGeometryObject.Edges.ToCurvesVisual3D();
                modelVisual3D.Children.Add(wires);
            }
            Visual3D = modelVisual3D;

            return Visual3D;
        }
    }
}
