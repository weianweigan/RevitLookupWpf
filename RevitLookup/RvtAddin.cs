/*
 * Created By WeiGan 2021.9.9
 * 1.Addin Class
 */

using System.Reflection;
using Autodesk.Revit.UI;
using RevitLookupWpf.Commands;
using RevitLookupWpf.Helpers;

namespace RevitLookupWpf
{
    public class RvtAddin : IExternalApplication
    {
        #region Public Methods
        public Result OnStartup(UIControlledApplication application)
        {
            //InitUI(application);
            CreateRibbonPanel(application);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Failed;
        }
        #endregion

        #region Private Methods
        //private void InitUI(UIControlledApplication application)
        //{
        //    string panelName = "RvtWpfLookup";

        //    //创建按钮
        //    var btns = CreatePushBtns(
        //        typeof(SnoopDBCommand),
        //        typeof(SnoopCurrentSelectionCommand),
        //        typeof(SnoopActiveDocCommand),
        //        typeof(SnoopUIApplicationCommand))
        //        .ToList();

        //    //创建Panel
        //    var ribbonPanel = application.CreateRibbonPanel(panelName);

        //    // Add the buttons to the panel
        //    btns.ForEach(btn => ribbonPanel.AddItem(btn));
        //}
        private static void CreateRibbonPanel(UIControlledApplication application)
        {
            var ribbonPanel = application.CreateRibbonPanel("Snoop");
            var pulldownButtonData = new PulldownButtonData("Options", "Revit Lookup");
            var pulldownButton = (PulldownButton)ribbonPanel.AddItem(pulldownButtonData);
            pulldownButton.Image = BitmapSourceConverter.ToImageSource(Resource.search, BitmapSourceConverter.ImageType.Small);
            pulldownButton.LargeImage = BitmapSourceConverter.ToImageSource(Resource.search, BitmapSourceConverter.ImageType.Large);
            AddPushButton(pulldownButton, typeof(SnoopDBCommand), "Snoop DB...");
            AddPushButton(pulldownButton, typeof(SnoopActiveDocCommand), "Snoop Active Document...");
            AddPushButton(pulldownButton, typeof(SnoopActiveViewCommand), "Snoop Active View...");
            AddPushButton(pulldownButton, typeof(SnoopCurrentSelectionCommand), "Snoop Current Selection...");
            AddPushButton(pulldownButton, typeof(SnoopFaceCommand), "Snoop Face...");
            AddPushButton(pulldownButton, typeof(SnoopEdgeCommand), "Snoop Edge...");
            AddPushButton(pulldownButton, typeof(SnoopGeometryCommand), "Snoop Geometry...");
            AddPushButton(pulldownButton, typeof(SnoopLinkedElementCommand), "Snoop Linked Element...");
            AddPushButton(pulldownButton, typeof(SnoopUIApplicationCommand), "Snoop UIApplication...");
        }

        private static PushButton AddPushButton(PulldownButton pullDownButton, Type command, string buttonText)
        {
            var buttonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
            return pullDownButton.AddPushButton(buttonData);
        }
        #endregion
    }
}
