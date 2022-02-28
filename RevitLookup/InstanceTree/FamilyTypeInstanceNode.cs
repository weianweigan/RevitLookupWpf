using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class FamilyTypeInstanceNode : InstanceNode<FamilyType>
    {
        public FamilyTypeInstanceNode(FamilyType rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                string result = rvtObject.Name;
                Name += $"({(string.IsNullOrEmpty(result)?result:rvtObject.GetHashCode())})";
            }
        }
    }
}
