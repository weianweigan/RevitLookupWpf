using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class FamilyParameterInstanceNode : InstanceNode<FamilyParameter>
    {
        public FamilyParameterInstanceNode(FamilyParameter rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Definition.Name})";
            }
        }
    }
}
