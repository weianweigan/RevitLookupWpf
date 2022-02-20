/*
 * Created By WeiGan 2021.9.9
 * 
 */

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.View;
using ArgumentException = System.ArgumentException;
using OperationCanceledException = Autodesk.Revit.Exceptions.OperationCanceledException;

namespace RevitLookupWpf.Commands
{
    [RvtCommandInfo(Name = "Snoop Point", Image = "search.png")]
    [Transaction(TransactionMode.Manual)]
    public class SnoopPointsCommand : RvtCommandBase
    {
        public override Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (commandData.Application.ActiveUIDocument == null)
            {
                message = Resource.NoActiveDocument;
                return Result.Cancelled;
            }
            
            Document doc = commandData.Application.ActiveUIDocument.Document;
          
            // Setting work plane
            using (Transaction transaction = new Transaction(doc, "sn"))
            {
                transaction.Start();
                if (doc.ActiveView is Autodesk.Revit.DB.ViewSection or View3D)
                {
                    Plane plane = Plane.CreateByNormalAndOrigin(doc.ActiveView.ViewDirection, doc.ActiveView.Origin);
                    doc.ActiveView.SketchPlane = SketchPlane.Create(doc, plane);
                }
                List<XYZ> xyzs = new List<XYZ>();
                TaskDialog.Show(Resource.AppName, "Select Ordered Points,Press Esc To Finish", TaskDialogCommonButtons.Ok);
                while (true)
                {
                    try
                    {
                        XYZ xyz = commandData.Application.ActiveUIDocument.Selection.PickPoint(
                            "Select Ordered Point,Press Esc to Cancel :D");
                        xyzs.Add(xyz);
                    }
                    catch (OperationCanceledException)
                    {
                        //user press esc
                        break;
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException(e.ToString());
                    }
                }

                if (xyzs.Count == 0)
                {
                    transaction.RollBack();
                    return Result.Succeeded;
                }
                var lookupWindow = new LookupWindow(commandData);
                if (xyzs.Count == 1) lookupWindow.SetRvtInstance(xyzs.FirstOrDefault());
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
