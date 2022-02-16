/*
 * Created By WeiGan 2021.9.9
 * 
 */

using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using ArgumentException = System.ArgumentException;

namespace RevitLookupWpf.Commands
{
    public abstract class RvtCommandBase : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Result result = Result.Succeeded;
            try
            {
                SnoopingContext.Init(commandData);
                result = SnoopClick(commandData, ref message, elements);
            }
            catch (Exception e)
            {
                result = Result.Failed;
                throw new  ArgumentException(e.ToString());
            }
            finally
            {
                //SnoopingContext.Dispose();
            }

            return result;
        }

        public abstract Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements);
    }
}
