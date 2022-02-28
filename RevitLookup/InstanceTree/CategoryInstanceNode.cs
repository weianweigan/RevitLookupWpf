using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class CategoryInstanceNode : InstanceNode<Category>
    {
        public CategoryInstanceNode(Category rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Name})";
            }
        }
    }
}
