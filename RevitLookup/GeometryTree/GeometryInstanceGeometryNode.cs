using Autodesk.Revit.DB;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class GeometryInstanceGeometryNode : GeometryNode<GeometryInstance>
    {
        public GeometryInstanceGeometryNode(GeometryInstance rvtGeometryObject) 
            : base(rvtGeometryObject)
        {
        }

        public override Visual3D LoadModel3D()
        {
            var instanceGeo = RvtGeometryObject.GetInstanceGeometry();
            if (instanceGeo == null)
            {
                return null;
            }

            var node = GeometryNode.CreateByGeometryObject(instanceGeo);
            var visual = node.LoadModel3D();

            if (visual == null)
            {
                return null;
            }

            Children.Add(node);
            
            return visual;
        }
    }
}
