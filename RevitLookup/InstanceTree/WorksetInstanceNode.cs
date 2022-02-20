using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class WorksetInstanceNode : InstanceNode<Workset>
    {
        public WorksetInstanceNode(Workset workset) : base(workset)
        {
            if (workset != null)
            {
                Name += $"({workset.Id})";
            }
        }
    }
}
