using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class EdgeArrayInstanceNode : InstanceNode<EdgeArray>
    {
        public EdgeArrayInstanceNode(EdgeArray rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Size})";
            }
        }
    }
}
