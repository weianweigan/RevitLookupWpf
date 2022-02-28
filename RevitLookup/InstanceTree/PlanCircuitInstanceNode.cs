using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class PlanCircuitInstanceNode : InstanceNode<PlanCircuit>
    {
        public PlanCircuitInstanceNode(PlanCircuit rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Area})";
            }
        }
    }
}
