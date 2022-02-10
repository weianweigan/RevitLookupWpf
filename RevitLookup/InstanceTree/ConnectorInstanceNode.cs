using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ConnectorInstanceNode : InstanceNode<Connector>
    {
        public ConnectorInstanceNode(Connector rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Id})";
            }
        }
    }
}
