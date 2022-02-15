using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class GeometryObjectInstanceNode : InstanceNode<GeometryObject>
    {
        public GeometryObjectInstanceNode(GeometryObject rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
#if R19 || R20
                Name += $"({rvtObjcet.GetHashCode()})";
#else
Name += $"({rvtObjcet.Id})";
#endif

            }
        }
    }
}
