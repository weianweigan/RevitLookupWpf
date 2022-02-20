using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class WorksetIdInstanceNode : InstanceNode<WorksetId>
    {
        private WorksetId elementId;
        public WorksetIdInstanceNode(WorksetId rvtObjcet) : base(rvtObjcet)
        {
            elementId = rvtObjcet;
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.IntegerValue})";
            }
        }
        public WorksetIdInstanceNode(WorksetId rvtObjcet,ExternalCommandData data) : base(rvtObjcet)
        {
            Data = data;
            elementId = rvtObjcet;
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.IntegerValue})";
            }
        }
        public InstanceNode ToWorksetInstanceNode()
        {
            InstanceNode node;
            Document doc = Data.Application.ActiveUIDocument.Document;
            WorksetTable worksetTable = doc.GetWorksetTable();
            Workset workset = worksetTable.GetWorkset(elementId);
            node = new WorksetInstanceNode(workset);
            return node;
        }
    }
}
