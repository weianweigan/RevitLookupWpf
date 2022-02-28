using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class PaperSizeInstanceNode : InstanceNode<PaperSize>
    {
        public PaperSizeInstanceNode(PaperSize rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Name})";
            }
        }
    }
}
