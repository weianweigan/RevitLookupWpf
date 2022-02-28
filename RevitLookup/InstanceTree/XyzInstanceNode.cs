using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class XYZInstanceNode : InstanceNode<XYZ>
    {
        public XYZInstanceNode(XYZ rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject})";
            }
        }
    }
}
