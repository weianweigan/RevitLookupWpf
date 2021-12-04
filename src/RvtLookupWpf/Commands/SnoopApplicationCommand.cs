/*
 * Created By WeiGan 2021.9.9
 * 
 */

using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using RvtLookupWpf.View;

namespace RvtLookupWpf.Commands
{
    [Transaction(TransactionMode.Manual)]
    [RvtCommandInfo(Name = "Snoop\nApplication", Image = "search.png")]
    public class SnoopApplicationCommand : RvtCommandBase
    {
        public override Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var windowHandle = commandData.Application.MainWindowHandle;
                var lookupWindow = new LookupWindow(windowHandle);
                lookupWindow.SetRvtInstance(commandData.Application);
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
