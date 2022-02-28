using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class WorksetIdInstanceNode : InstanceNode<WorksetId>
    {
        private WorksetId elementId;
        public WorksetIdInstanceNode(WorksetId rvtObject) : base(rvtObject)
        {
            elementId = rvtObject;
            if (rvtObject != null)
            {
                Name += $"({rvtObject.IntegerValue})";
            }
        }
        public InstanceNode ToWorksetInstanceNode()
        {
            InstanceNode node;
            Document doc = SnoopingContext.Instance.CommandData.Application.ActiveUIDocument.Document;
            WorksetTable worksetTable = doc.GetWorksetTable();
            Workset workset = worksetTable.GetWorkset(elementId);
            node = new WorksetInstanceNode(workset);
            return node;
        }
    }
}
