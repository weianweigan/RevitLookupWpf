using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using RevitLookupWpf.GeometryConverter;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public class BoundingBoxXYZGeometryNode : GeometryNode<BoundingBoxXYZ>
    {
        public BoundingBoxXYZGeometryNode(BoundingBoxXYZ rvtGeometryObject) 
            : base(rvtGeometryObject)
        {
            Name = $"{rvtGeometryObject.GetType().Name}";
        }

        public override Visual3D LoadModel3D()
        {
            Visual3D = new BoundingBoxVisual3D()
            {
                BoundingBox = new Rect3D()
                {
                    Location = RvtGeometryObject.Min.ToPoint3D(),
                    Size = (RvtGeometryObject.Max - RvtGeometryObject.Min).ToSize3D()
                }
            };

            return Visual3D;
        }
    }
}
