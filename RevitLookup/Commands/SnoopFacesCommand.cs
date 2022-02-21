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
    [RvtCommandInfo(Name = "Snoop Face", Image = "search.png")]
    [Transaction(TransactionMode.Manual)]
    public class SnoopFacesCommand : RvtCommandBase
    {
        public override Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            if (uidoc == null)
            {
                message = Resource.NoActiveDocument;
                return Result.Cancelled;
            }
            var lookupWindow = new LookupWindow(commandData);
            List<GeometryObject> geos = new List<GeometryObject>();
            TaskDialogResult result = MessageUtils.QuestionMsg("Selection Mode:","Single","Normal","Order","Cancel");
            Document doc = uidoc.Document;
            switch (result)
            {
                case TaskDialogResult.CommandLink1:
                    geos = PickSingle(uidoc);
                    break;
                case TaskDialogResult.CommandLink2:
                    geos = PickNormal(commandData);
                    break;
                case TaskDialogResult.CommandLink3:
                    geos = PickOrder(commandData);
                    break;
                case TaskDialogResult.CommandLink4:
                    return Result.Succeeded;
            }
            if (geos.Count == 0) return Result.Cancelled;
            if(geos.Count==1) lookupWindow.SetRvtInstance(geos.FirstOrDefault());
            else lookupWindow.SetRvtInstance(geos);
            lookupWindow.Show();

            return Result.Succeeded;
        }

        List<GeometryObject> PickSingle(UIDocument uidoc)
        {
            List<GeometryObject> geos = new List<GeometryObject>();
            try
            {
                Reference r = uidoc.Selection.PickObject(ObjectType.Face);
                var geometryObject = uidoc.Document.GetElement(r).GetGeometryObjectFromReference(r);
                geos.Add(geometryObject);
            }
            catch (OperationCanceledException)
            {
                //ignore
            }
            return geos;
        }
        List<GeometryObject> PickNormal(ExternalCommandData data)
        {
            List<GeometryObject> geos = new List<GeometryObject>();
            try
            {
                var refElem = data.Application.ActiveUIDocument.Selection.PickObjects(ObjectType.Face);
                foreach (Reference r in refElem)
                {
                    var geometryObject = data.Application.ActiveUIDocument.Document.GetElement(r).GetGeometryObjectFromReference(r);
                    geos.Add(geometryObject);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                throw new ArgumentException(e.ToString());
            }

            return geos;
        }
        List<GeometryObject> PickOrder(ExternalCommandData data)
        {
            List<GeometryObject> geos = new List<GeometryObject>();
            TaskDialog.Show(Resource.AppName, "Select Ordered Edges,Press Esc To Finish", TaskDialogCommonButtons.Ok);
            while (true)
            {
                try
                {
                    var refElem = data.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Face);
                    var geometryObject = data.Application.ActiveUIDocument.Document.GetElement(refElem).GetGeometryObjectFromReference(refElem);
                    geos.Add(geometryObject);
                }
                catch (Exception)
                {
                    //user press esc
                    break;
                }
            }

            return geos;
        }
    }
}
