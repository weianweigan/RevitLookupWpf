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
    [Transaction(TransactionMode.Manual)]
    [RvtCommandInfo(Name ="Snoop Point \n On Element", Image = "search.png")]
    public class SnoopPointOnEleCommand : RvtCommandBase
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
                var lookupWindow = new LookupWindow(commandData);

                var selections = uiDoc.Selection.GetElementIds().Select(p => uiDoc.Document.GetElement(p)).ToList();

                if (!selections.Any())
                {
                    selections = new List<Element>();
                    IList<Reference> references = uiDoc.Selection.PickObjects(ObjectType.PointOnElement,Resource.PickElements);
                    foreach (Reference r in references)
                    {
                        selections.Add(uiDoc.Document.GetElement(r));
                    }
                }
                if (selections.Count == 1)
                {
                    lookupWindow.SetRvtInstance(selections.First());
                }
                else
                {
                    lookupWindow.SetRvtInstance(selections);
                }

                lookupWindow.Show();
            }
            catch (OperationCanceledException)
            {
                //ignore user press esc
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.ToString());
            }

            return Result.Succeeded;
        }
    }
}
