using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class CategoryInstanceNode : InstanceNode<Category>
    {
        public CategoryInstanceNode(Category rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
