using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

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
