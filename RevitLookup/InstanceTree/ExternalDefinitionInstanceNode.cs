using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ExternalDefinitionInstanceNode : InstanceNode<ExternalDefinition>
    {
        public ExternalDefinitionInstanceNode(ExternalDefinition rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
