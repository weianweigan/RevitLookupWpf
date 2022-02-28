using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class FamilyParameterInstanceNode : InstanceNode<FamilyParameter>
    {
        public FamilyParameterInstanceNode(FamilyParameter rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Definition.Name})";
            }
        }
    }
}
