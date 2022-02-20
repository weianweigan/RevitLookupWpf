/*
 * Created By WeiGan 2021.9.9
 * 
 */

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.View;
using OperationCanceledException = Autodesk.Revit.Exceptions.OperationCanceledException;

namespace RevitLookupWpf.Commands
{
    [RvtCommandInfo(Name = "Snoop \nGeometry Element", Image = "search.png")]
    [Transaction(TransactionMode.Manual)]
    public class SnoopGeometryCommand : RvtCommandBase
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
                var lookupWindow = new LookupWindow(commandData);
                var refElem = commandData.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Element);
                GeometryElement geometryElement = commandData.Application.ActiveUIDocument.Document.GetElement(refElem)
                    .get_Geometry(new Options());
                lookupWindow.SetRvtInstance(geometryElement);
                lookupWindow.Show();
            }
            catch (NullReferenceException)
            {
                TaskDialog.Show(Resource.AppName, Resource.NoGeometry, TaskDialogCommonButtons.Ok);
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
