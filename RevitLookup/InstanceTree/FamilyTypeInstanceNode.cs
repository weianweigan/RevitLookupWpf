using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class FamilyTypeInstanceNode : InstanceNode<FamilyType>
    {
        public FamilyTypeInstanceNode(FamilyType rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                string result = rvtObjcet.Name;
                Name += $"({(string.IsNullOrEmpty(result)?result:rvtObjcet.GetHashCode())})";
            }
        }
    }
}
