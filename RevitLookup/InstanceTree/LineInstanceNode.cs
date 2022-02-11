using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class LineInstanceNode : InstanceNode<Line>
    {
        public LineInstanceNode(Line rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.ApproximateLength})";
            }
        }
    }
}
