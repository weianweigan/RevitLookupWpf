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
    [RvtCommandInfo(Name ="Snoop\nSelection", Image = "search.png")]
    public class SnoopCurrentSeletionCommand : RvtCommandBase
    {
        public override Result SnoopClick(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            if (uiDoc == null)
            {
                message = "没有活动文档";
                return Result.Cancelled;
            }

            try
            {
                var windowHandle = commandData.Application.MainWindowHandle;
                var lookupWindow = new LookupWindow(windowHandle);

                var selections = uiDoc.Selection.GetElementIds().Select(p => uiDoc.Document.GetElement(p)).ToList();

                if (!selections.Any())
                {
                    message = "当前未选择元素";
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

                lookupWindow.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }

            return Result.Succeeded;
        }
    }
}
