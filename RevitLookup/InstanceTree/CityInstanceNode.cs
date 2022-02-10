using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class CityInstanceNode : InstanceNode<City>
    {
        public CityInstanceNode(City rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
