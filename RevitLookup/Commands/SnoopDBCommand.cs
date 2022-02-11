/*
 * Created By WeiGan 2021.9.9
 * 
 */

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitLookupWpf.View;

namespace RevitLookupWpf.Commands
{
    [Transaction(TransactionMode.Manual)]
    [RvtCommandInfo(Name = "Snoop\nDB", Image = "search.png")]
    public class SnoopDBCommand : RvtCommandBase
    {
        public override Result SnoopClick(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            if (uiDoc == null)
            {
                message = Resource.NoActiveDocument;
                return Result.Cancelled;
            }

            try
            {
                var windowHandle = commandData.Application.MainWindowHandle;
                var lookupWindow = new LookupWindow(windowHandle);
                var document = commandData.Application.ActiveUIDocument.Document;
                var elementTypes = new FilteredElementCollector(document).WhereElementIsElementType();
                var elementInstances = new FilteredElementCollector(document).WhereElementIsNotElementType();
                var elementsCollector = elementTypes.UnionWith(elementInstances);
                var seElements = elementsCollector.ToElements();
                lookupWindow.SetRvtInstance(seElements);
                lookupWindow.Show();

            }
            catch (Exception e)
            {
                throw new ArgumentException(e.ToString());
            }

            return Result.Succeeded;
        }
    }
}
