using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class DocumentInstanceNode : InstanceNode<Document>
    {
        public DocumentInstanceNode(Document rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Title})";
            }
        }
    }
}
