/*
 * Created By WeiGan 2021.9.9
 * 
 */

using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using RvtLookupWpf.View;
using Autodesk.Revit.Attributes;

namespace RvtLookupWpf.Commands
{
    [RvtCommandInfo(Name = "Snoop\nActiveDoc", Image = "search.png")]
    [Transaction(TransactionMode.Manual)]
    public class SnoopActiveDocCommand : RvtCommandBase
    {
        public override Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (commandData.Application.ActiveUIDocument == null)
            {
                message = "没有活动文档";
                return Result.Cancelled;
            }

            try
            {                
                var windowHandle = commandData.Application.MainWindowHandle;
                var lookupWindow = new LookupWindow(windowHandle);
                lookupWindow.SetRvtInstance(commandData.Application.ActiveUIDocument.Document);
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
