using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class FailureDefinitionAccessorInstanceNode : InstanceNode<FailureDefinitionAccessor>
    {
        public FailureDefinitionAccessorInstanceNode(FailureDefinitionAccessor rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.GetSeverity()+" "+rvtObjcet.GetDescriptionText()})";
            }
        }
    }
}
