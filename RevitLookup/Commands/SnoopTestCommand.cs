/*
 * Created By WeiGan 2021.9.9
 * 
 */

using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.View;

namespace RevitLookupWpf.Commands
{
    [Transaction(TransactionMode.Manual)]
    [RvtCommandInfo(Name = "Snoop\\Test Command", Image = "search.png")]
    public class SnoopTestCommand : RvtCommandBase
    {
        public override Result SnoopClick(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            Reference re = uiDoc.Selection.PickObject(ObjectType.Element);
            Element element = uiDoc.Document.GetElement(re);
            ElementId elementId = element.GetTypeId();
            Element wallType = uiDoc.Document.GetElement(elementId);
            MessageBox.Show(wallType.Name);
            return Result.Succeeded;
        }
    }
}
