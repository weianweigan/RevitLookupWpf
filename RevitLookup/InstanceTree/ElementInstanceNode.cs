using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ElementInstanceNode : InstanceNode<Element>
    {
        public ElementInstanceNode(Element rvtObjcet,bool isRoot) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                if(isRoot) Name += $"({rvtObjcet.Id.IntegerValue})";
                else Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
