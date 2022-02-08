/*
 * Created By WeiGan 2021.9.9
 * 1.Addin Class
 */

using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using RevitLookupWpf.Commands;

namespace RevitLookupWpf
{
    public abstract class RvtAddinBase : IExternalApplication
    {

        public RvtAddinBase()
        {
            ResourcesDirectory = Path.Combine(Path.GetDirectoryName(typeof(RvtAddinBase).Assembly.Location), "Resources");
        }

        public string ResourcesDirectory { get; }

        #region Public Abstract Methods
        public abstract Result OnShutdown(UIControlledApplication application);

        public abstract Result OnStartup(UIControlledApplication application);
        #endregion

        #region Protect Methods
        protected PushButtonData CreatePushBtn(Type cmdType)
        {
            var cmdInfo = cmdType.GetCustomAttribute<RvtCommandInfoAttribute>();

            var pushBtn = new PushButtonData(
                cmdInfo?.Name ?? cmdType.Name,
                cmdInfo?.Name ?? cmdType.Name,
                cmdType.Assembly.Location,
                cmdType.FullName);

            if (!string.IsNullOrEmpty(cmdInfo?.Image))
            {
                var location = Path.Combine(ResourcesDirectory, cmdInfo.Image);
                if (File.Exists(location))
                {
                    pushBtn.LargeImage = new BitmapImage(new Uri(location));
                }
            }

            return pushBtn;
        }

        protected IEnumerable<PushButtonData> CreatePushBtns(params Type[] cmdTypes)
        {
            foreach (var type in cmdTypes)
            {
                yield return CreatePushBtn(type);
            }
        }

        protected PushButtonData CreatePushBtn<TCommand>()
            where TCommand:RvtCommandBase
        {
            var cmdType = typeof(TCommand);
            return CreatePushBtn(cmdType);
        }
        #endregion
    }
}
