using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ConnectorInstanceNode : InstanceNode<Connector>
    {
        public ConnectorInstanceNode(Connector rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Id})";
            }
        }
    }
}
