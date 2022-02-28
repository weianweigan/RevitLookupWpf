using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class FailureDefinitionAccessorInstanceNode : InstanceNode<FailureDefinitionAccessor>
    {
        public FailureDefinitionAccessorInstanceNode(FailureDefinitionAccessor rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                string content = rvtObject.GetDescriptionText().Replace("\n", "").Replace("\r", "");
                Name += $"({rvtObject.GetSeverity()+" "+ content})";
            }
        }
    }
}
