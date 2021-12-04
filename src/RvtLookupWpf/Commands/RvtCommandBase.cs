/*
 * Created By WeiGan 2021.9.9
 * 
 */

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System;

namespace RvtLookupWpf.Commands
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
            catch (Exception)
            {
                result = Result.Failed;
                throw;
            }
            finally
            {
                SnoopingContext.Dispose();
            }

            return result;
        }

        public abstract Result SnoopClick(ExternalCommandData commandData, ref string message, ElementSet elements);
    }
}
