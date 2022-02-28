using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class GeometryObjectInstanceNode : InstanceNode<GeometryObject>
    {
        public GeometryObjectInstanceNode(GeometryObject rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
#if R19 || R20
                Name += $"({rvtObjcet.GetHashCode()})";
#else
Name += $"({rvtObject.Id})";
#endif

            }
        }
    }
}
