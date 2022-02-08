using Autodesk.Revit.UI;

namespace RevitLookup
{
    public class SnoopingContext
    {
        #region Ctor
        private SnoopingContext(ExternalCommandData commandData)
        {
            CommandData = commandData;
        }
        #endregion

        public ExternalCommandData CommandData { get; }

        #region Static Property
        public static SnoopingContext Instance { get; private set; }
        #endregion

        #region Static Methods
        public static void Init(Autodesk.Revit.UI.ExternalCommandData commandData)
        {
            Instance = new SnoopingContext(commandData);
        }

        public static void Dispose()
        {
            Instance = null;
        }
        #endregion
    }
}
