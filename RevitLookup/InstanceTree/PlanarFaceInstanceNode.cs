using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class PlanarFaceInstanceNode : InstanceNode<PlanarFace>
    {
        public PlanarFaceInstanceNode(PlanarFace rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Area})";
            }
        }
    }
}
