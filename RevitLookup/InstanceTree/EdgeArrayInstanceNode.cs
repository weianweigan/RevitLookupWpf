using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class EdgeArrayInstanceNode : InstanceNode<EdgeArray>
    {
        public EdgeArrayInstanceNode(EdgeArray rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Size})";
            }
        }
    }
}
