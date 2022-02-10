using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class XYZInstanceNode : InstanceNode<XYZ>
    {
        public XYZInstanceNode(XYZ rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.X},{rvtObjcet.Y},{rvtObjcet.Z})";
            }
        }
    }
}
