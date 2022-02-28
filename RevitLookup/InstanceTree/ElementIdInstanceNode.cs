using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.InstanceTree
{
    public class ElementIdInstanceNode : InstanceNode<ElementId>
    {
        private ElementId elementId;
        public ElementIdInstanceNode(ElementId rvtObject) : base(rvtObject)
        {
            elementId = rvtObject;
            if (rvtObject != null)
            {
                Name += $"({rvtObject.IntegerValue})";
            }
        }
        public InstanceNode ToElementInstanceNode(bool isRoot)
        {
            InstanceNode node;
            Document doc = SnoopingContext.Instance.CommandData.Application.ActiveUIDocument.Document;
            Element e = doc.GetElement(elementId);
            if (e != null) node = new ElementInstanceNode(e,isRoot);
            else node = new ElementIdInstanceNode(elementId);
            return node;
        }
    }
}
