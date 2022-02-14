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
    public class SnoopEdgesCommand : RvtCommandBase
    {
        public override Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (commandData.Application.ActiveUIDocument == null)
            {
                message = Resource.NoActiveDocument;
                return Result.Cancelled;
            }
            var windowHandle = commandData.Application.MainWindowHandle;
            var lookupWindow = new LookupWindow(windowHandle);
            List<GeometryObject> geos = new List<GeometryObject>();
            TaskDialog.Show(Resource.AppName, "Select Ordered Edges,Press Esc To Finish", TaskDialogCommonButtons.Ok);
            while (true)
            {
                try
                {
                    var refElem = commandData.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Edge);
                    var geometryObject = commandData.Application.ActiveUIDocument.Document.GetElement(refElem).GetGeometryObjectFromReference(refElem);
                    geos.Add(geometryObject);
                }
                catch (Exception)
                {
                    //user press esc
                    break;
                }
            }
            if (geos.Count == 0) return Result.Cancelled;
            if(geos.Count==1) lookupWindow.SetRvtInstance(geos.FirstOrDefault());
            else lookupWindow.SetRvtInstance(geos);
            lookupWindow.Show();

            return Result.Succeeded;
        }
    }
}
