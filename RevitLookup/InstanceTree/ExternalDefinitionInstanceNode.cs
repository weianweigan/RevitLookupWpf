using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ExternalDefinitionInstanceNode : InstanceNode<ExternalDefinition>
    {
        public ExternalDefinitionInstanceNode(ExternalDefinition rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Name})";
            }
        }
    }
}
