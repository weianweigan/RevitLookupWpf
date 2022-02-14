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
                List<XYZ> xyzs = new List<XYZ>();
                TaskDialog.Show(Resource.AppName, "Select Ordered Points,Press Esc to Cancel", TaskDialogCommonButtons.Ok);
                while (true)
                {
                    try
                    {
                        XYZ xyz = commandData.Application.ActiveUIDocument.Selection.PickPoint(
                            "Select Ordered Point,Press Esc to Cancel :D");
                      xyzs.Add(xyz);
                    }
                    catch (Exception)
                    {
                        //user press esc
                        break;
                    }
                }

                if (xyzs.Count == 0)
                {
                    transaction.RollBack();
                    return Result.Succeeded;
                }
                if (xyzs.Count == 1) lookupWindow.SetRvtInstance(xyzs.First());
                else if(xyzs.Any())
                {
                    lookupWindow.SetRvtInstance(xyzs);
                }
                lookupWindow.Show();
                transaction.RollBack();
            }

            return Result.Succeeded;
        }
    }
}
