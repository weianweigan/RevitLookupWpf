/*
 * Created By WeiGan 2021.9.10
 * 属性列表
 */

using RevitLookup.PropertySys.BaseProperty;

namespace RevitLookup.PropertySys
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
