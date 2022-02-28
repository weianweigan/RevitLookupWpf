using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class RibbonPanelInstanceNode : InstanceNode<RibbonPanel>
    {
        public RibbonPanelInstanceNode(RibbonPanel rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Name})";
            }
        }
    }
}
