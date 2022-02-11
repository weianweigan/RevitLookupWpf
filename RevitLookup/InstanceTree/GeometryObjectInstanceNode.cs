using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class GeometryObjectInstanceNode : InstanceNode<GeometryObject>
    {
        public GeometryObjectInstanceNode(GeometryObject rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Id})";
            }
        }
    }
}
