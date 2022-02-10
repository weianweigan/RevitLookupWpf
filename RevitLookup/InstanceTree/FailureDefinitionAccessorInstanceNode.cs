using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class FailureDefinitionAccessorInstanceNode : InstanceNode<FailureDefinitionAccessor>
    {
        public FailureDefinitionAccessorInstanceNode(FailureDefinitionAccessor rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                string content = rvtObjcet.GetDescriptionText().Replace("\n", "").Replace("\r", "");
                Name += $"({rvtObjcet.GetSeverity()+" "+ content})";
            }
        }
    }
}
