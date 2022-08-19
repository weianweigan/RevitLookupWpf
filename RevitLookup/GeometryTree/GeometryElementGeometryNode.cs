using Autodesk.Revit.DB;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class GeometryElementGeometryNode : GeometryNode<GeometryElement>
    {
        public GeometryElementGeometryNode(GeometryElement rvtGeometryObject) 
            : base(rvtGeometryObject)
        {
        }

        public override Visual3D LoadModel3D()
        {
            var modelVisual3d = new ModelVisual3D();

            foreach (var geoObj in RvtGeometryObject)
            {
                var geoNode = GeometryNode.CreateByGeometryObject(geoObj);
                Children.Add(geoNode);

                var visual = geoNode.LoadModel3D();
                if (visual != null)
                    modelVisual3d.Children.Add(visual);
            }

            return modelVisual3d;
        }
    }
}
