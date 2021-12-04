/*
 * Created By WeiGan 2021.9.9
 * 1.Addin Class
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using Autodesk.Revit.UI;
using RvtLookupWpf.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RvtLookupWpf
{
    public class RvtAddin : RvtAddinBase
    {
        #region Public Methods
        public override Result OnStartup(UIControlledApplication application)
        {
            InitUI(application);

            return Result.Succeeded;
        }

        public override Result OnShutdown(UIControlledApplication application)
        {
            return Result.Failed;
        }
        #endregion

        #region Private Methods
        private void InitUI(UIControlledApplication application)
        {
            string tabName = "RvtWpfLookup";
            string panelName = "RvtWpfLookup";

            //创建Tab
            application.CreateRibbonTab(tabName);

            //创建按钮
            var btns = CreatePushBtns(
                typeof(SnoopDBCommand),
                typeof(SnoopCurrentSeletionCommand),
                typeof(SnoopActiveDocCommand),
                typeof(SnoopApplicationCommand))
                .ToList();

            //创建Panel
            var ribbonPanel = application.CreateRibbonPanel(tabName, panelName);

            // Add the buttons to the panel
            btns.ForEach(btn => ribbonPanel.AddItem(btn));
        }
        #endregion
    }
}
