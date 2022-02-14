/*
 * Created By WeiGan 2021.9.9
 * 
 */

using System.Windows.Interop;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitLookupWpf.View;
using RevitLookupWpf.ViewModel;
using OperationCanceledException = Autodesk.Revit.Exceptions.OperationCanceledException;

namespace RevitLookupWpf.Commands
{
    [Transaction(TransactionMode.Manual)]
    [RvtCommandInfo(Name ="Snoop\nSearch", Image = "search.png")]
    public class SnoopSearchCommand : RvtCommandBase
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
                SnoopSearchViewModel vm = new SnoopSearchViewModel(commandData);
                SearchWindow wd = new SearchWindow(vm);
                var windowHandle = commandData.Application.MainWindowHandle;
                var windowInteropHelper = new WindowInteropHelper(wd);
                windowInteropHelper.Owner = windowHandle;
                wd.Show();
                return Result.Succeeded;
                
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.ToString());
            }
        }
    }
}
