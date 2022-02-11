using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class EdgeInstanceNode : InstanceNode<Edge>
    {
        public EdgeInstanceNode(Edge rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.ApproximateLength})";
            }
        }
    }
}
