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
    [RvtCommandInfo(Name = "Snoop\nActiveView", Image = "search.png")]
    [Transaction(TransactionMode.Manual)]
    public class SnoopActiveViewCommand : RvtCommandBase
    {
        public override Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var windowHandle = commandData.Application.MainWindowHandle;
                var lookupWindow = new LookupWindow(windowHandle);
                lookupWindow.SetRvtInstance(commandData.Application.ActiveUIDocument.Document.ActiveView);
                lookupWindow.ShowDialog();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.ToString());
            }

            return Result.Succeeded;
        }
    }
}
