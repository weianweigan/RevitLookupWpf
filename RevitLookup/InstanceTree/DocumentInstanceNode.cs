using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class DocumentInstanceNode : InstanceNode<Document>
    {
        public DocumentInstanceNode(Document rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Title})";
            }
        }
    }
}
