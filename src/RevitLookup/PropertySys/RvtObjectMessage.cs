/*
 * Created By WeiGan 2021.12.24
 * a message class for snoop next revit object
 */

namespace RevitLookupWpf.PropertySys
{
    internal class RvtObjectMessage
    {
        public RvtObjectMessage(object obj)
        {
            RvtObject  =obj;
        }

        public object RvtObject { get;}
    }
}
