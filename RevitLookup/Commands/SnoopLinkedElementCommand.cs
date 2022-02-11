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
    [Transaction(TransactionMode.Manual)]
    [RvtCommandInfo(Name ="Snoop\nLink Element", Image = "search.png")]
    public class SnoopLinkedElementCommand : RvtCommandBase
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
                List<Element> selections = new List<Element>();
                IList<Reference> references = uiDoc.Selection.PickObjects(ObjectType.LinkedElement,Resource.PickLinkElements);
                foreach (Reference r in references)
                {
                    RevitLinkInstance elem = uiDoc.Document.GetElement(r.ElementId) as RevitLinkInstance;
                    Document docLinked = elem.GetLinkDocument();
                    Element linkElement = docLinked.GetElement(r.LinkedElementId);
                    selections.Add(linkElement);
                }
                if (!selections.Any())
                {
                    return Result.Cancelled;
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
            catch (Exception)
            {
                throw;
            }

            return Result.Succeeded;
        }
    }
}
