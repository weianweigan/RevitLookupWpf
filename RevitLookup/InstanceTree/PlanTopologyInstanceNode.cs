using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class PlanTopologyInstanceNode : InstanceNode<PlanTopology>
    {
        public PlanTopologyInstanceNode(PlanTopology rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Level.Name})";
            }
        }
    }
}
