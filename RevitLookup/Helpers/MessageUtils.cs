using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace RevitLookupWpf.Helpers
{
    public static class MessageUtils
    {
        /// <summary>
        /// Return Result Has Select Element Current Or Not
        /// </summary>
        /// <param name="Caption">Title Question</param>
        /// <returns></returns>
        public static TaskDialogResult QuestionMsg(string Caption, string op1, string op2,string op3,string op4)
        {

            var dialog = new TaskDialog(Caption);
            dialog.MainIcon = TaskDialogIcon.TaskDialogIconNone;
            dialog.MainInstruction = Caption;
            dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, op1);
            dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, op2);
            dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink3, op3);
            dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink4, op4);
            return dialog.Show();
        }
    }
}
