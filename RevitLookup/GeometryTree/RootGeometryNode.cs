using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class RootGeometryNode : GeometryNode
    {
        public RootGeometryNode()
        {
            Name = "Geometry Items";
        }

        public override Visual3D LoadModel3D()
        {
            throw new NotImplementedException();
        }
    }
}
