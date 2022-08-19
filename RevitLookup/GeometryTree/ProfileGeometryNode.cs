using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class ProfileGeometryNode : GeometryNode<Profile>
    {
        public ProfileGeometryNode(Profile rvtGeometryObject)
            : base(rvtGeometryObject)
        {
        }

        public override Visual3D LoadModel3D()
        {
            Visual3D = RvtGeometryObject.Curves.ToCurvesVisual3D();
            return Visual3D;
        }
    }
}
