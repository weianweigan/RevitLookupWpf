using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class ElementInstanceNode : InstanceNode<Element>
    {
        public ElementInstanceNode(Element rvtObject,bool isRoot) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                if(isRoot) Name += $"({rvtObject.Id.IntegerValue})";
                else Name += $"({rvtObject.Name})";
            }
        }
    }
}
