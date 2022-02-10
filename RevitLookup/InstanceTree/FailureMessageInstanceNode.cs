using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class FailureMessageInstanceNode : InstanceNode<FailureMessage>
    {
        public FailureMessageInstanceNode(FailureMessage rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.GetSeverity()+" "+rvtObjcet.GetDescriptionText()})";
            }
        }
    }
}
