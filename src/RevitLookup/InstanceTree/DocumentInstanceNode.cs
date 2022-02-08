using Autodesk.Revit.DB;

namespace RevitLookup.InstanceTree
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
