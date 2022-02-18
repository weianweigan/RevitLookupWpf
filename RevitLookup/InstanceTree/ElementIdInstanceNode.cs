using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class ElementIdInstanceNode : InstanceNode<ElementId>
    {
        private ExternalCommandData Data;
        private ElementId elementId;
        public ElementIdInstanceNode(ElementId rvtObjcet) : base(rvtObjcet)
        {
            elementId = rvtObjcet;
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.IntegerValue})";
            }
        }
        public ElementIdInstanceNode(ElementId rvtObjcet,ExternalCommandData data) : base(rvtObjcet)
        {
            Data = data;
            elementId = rvtObjcet;
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.IntegerValue})";
            }
        }
        public InstanceNode ToElementInstanceNode()
        {
            InstanceNode node;
            Document doc = Data.Application.ActiveUIDocument.Document;
            Element e = doc.GetElement(elementId);
            if (e != null) node = new ElementInstanceNode(e);
            else node = new ElementIdInstanceNode(elementId);
            return node;
        }
    }
}
