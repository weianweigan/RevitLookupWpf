using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ElementIdInstanceNode : InstanceNode<ElementId>
    {
        public ElementIdInstanceNode(ElementId rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.IntegerValue})";
            }
        }
    }
}
