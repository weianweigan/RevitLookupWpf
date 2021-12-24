/*
 * Created By WeiGan 2021.12.24
 * a message class for snoop next revit object
 */

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RvtLookupWpf.PropertySys
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
