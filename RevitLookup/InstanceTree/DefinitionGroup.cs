using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class DefinitionGroupInstanceNode : InstanceNode<DefinitionGroup>
    {
        public DefinitionGroupInstanceNode(DefinitionGroup rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Name})";
            }
        }
    }
}
