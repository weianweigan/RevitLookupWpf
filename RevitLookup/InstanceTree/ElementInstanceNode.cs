using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ElementInstanceNode : InstanceNode<Element>
    {
        public ElementInstanceNode(Element rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
