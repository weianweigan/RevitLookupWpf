using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class CityInstanceNode : InstanceNode<City>
    {
        public CityInstanceNode(City rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Name})";
            }
        }
    }
}
