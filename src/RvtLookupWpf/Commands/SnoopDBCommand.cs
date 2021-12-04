/*
 * Created By WeiGan 2021.9.9
 * 
 */

using System;
using System.Linq;
using System.Text;
using Autodesk.Revit.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using RvtLookupWpf.View;

namespace RvtLookupWpf.Commands
{
    [Transaction(TransactionMode.Manual)]
    [RvtCommandInfo(Name = "Snoop\nDB", Image = "search.png")]
    public class SnoopDBCommand : RvtCommandBase
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

                var idoc = uiDoc.Document;

                lookupWindow.SetRvtInstance(idoc);
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
