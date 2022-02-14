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
    [RvtCommandInfo(Name = "Snoop Point", Image = "search.png")]
    [Transaction(TransactionMode.Manual)]
    public class SnoopPointCommand : RvtCommandBase
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
                Document doc = commandData.Application.ActiveUIDocument.Document;
                var lookupWindow = new LookupWindow(windowHandle);
                // Setting work plane
                using (Transaction transaction = new Transaction(doc, "sn"))
                {
                    transaction.Start();
                    Plane plane = Plane.CreateByNormalAndOrigin(doc.ActiveView.ViewDirection, doc.ActiveView.Origin);
                    doc.ActiveView.SketchPlane = SketchPlane.Create(doc, plane);
                    doc.ActiveView.ShowActiveWorkPlane();
                    var xyz = commandData.Application.ActiveUIDocument.Selection.PickPoint();
                    lookupWindow.SetRvtInstance(xyz);
                    lookupWindow.Show();
                    transaction.RollBack();
                }
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
