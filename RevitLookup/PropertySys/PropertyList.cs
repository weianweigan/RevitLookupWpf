/*
 * Created By WeiGan 2021.9.10
 * 属性列表
 */

using RevitLookupWpf.InstanceTree;
using RevitLookupWpf.PropertySys.BaseProperty;

namespace RevitLookupWpf.PropertySys
{
    public class PropertyList : List<PropertyBase>
    {
        public PropertyList(object rvtObject, InstanceNode parent)
        {
            RvtObject = rvtObject;
            Parent = parent;
        }

        public object RvtObject { get; }

        public InstanceNode Parent { get; }

        /// <summary>
        /// Inheritance 
        /// </summary>
        public string Inheri { get; set; }
    }
}
