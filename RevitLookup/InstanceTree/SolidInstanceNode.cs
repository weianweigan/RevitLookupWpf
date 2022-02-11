using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class SolidInstanceNode : InstanceNode<Solid>
    {
        public SolidInstanceNode(Solid rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.SurfaceArea})";
            }
        }
    }
}
