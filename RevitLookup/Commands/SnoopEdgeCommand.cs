/*
 * Created By WeiGan 2021.9.9
 * 
 */

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitLookupWpf.View;
using OperationCanceledException = Autodesk.Revit.Exceptions.OperationCanceledException;

namespace RevitLookupWpf.Commands
{
    [RvtCommandInfo(Name = "Snoop Edge", Image = "search.png")]
    [Transaction(TransactionMode.Manual)]
    public class SnoopEdgeCommand : RvtCommandBase
    {
        public override Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (commandData.Application.ActiveUIDocument == null)
            {
                message = Resource.NoActiveDocument;
                return Result.Cancelled;
            }

            try
            {                
                var windowHandle = commandData.Application.MainWindowHandle;
                var lookupWindow = new LookupWindow(windowHandle);
                var refElem = commandData.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Edge);
                var geometryObject = commandData.Application.ActiveUIDocument.Document.GetElement(refElem).GetGeometryObjectFromReference(refElem);
                lookupWindow.SetRvtInstance(geometryObject);
                lookupWindow.Show();
            }
            catch (OperationCanceledException)
            {
                //ignore user press esc
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.ToString());
            }

            return Result.Succeeded;
        }
    }
}
