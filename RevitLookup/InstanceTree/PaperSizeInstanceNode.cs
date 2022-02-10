using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class PaperSizeInstanceNode : InstanceNode<PaperSize>
    {
        public PaperSizeInstanceNode(PaperSize rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
