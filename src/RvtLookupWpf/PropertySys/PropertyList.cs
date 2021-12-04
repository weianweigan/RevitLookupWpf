/*
 * Created By WeiGan 2021.9.10
 * 属性列表
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvtLookupWpf.PropertySys
{
    public class PropertyList : List<PropertyBase>
    {
        public PropertyList(object rvtObject)
        {
            RvtObject = rvtObject;
        }

        public object RvtObject { get; }

        /// <summary>
        /// 继承层次
        /// </summary>
        public string Deri { get; set; }
    }
}
