using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class FailureMessageInstanceNode : InstanceNode<FailureMessage>
    {
        public FailureMessageInstanceNode(FailureMessage rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.GetSeverity()+" "+rvtObject.GetDescriptionText()})";
            }
        }
    }
}
