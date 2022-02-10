using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class RibbonPanelInstanceNode : InstanceNode<RibbonPanel>
    {
        public RibbonPanelInstanceNode(RibbonPanel rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
