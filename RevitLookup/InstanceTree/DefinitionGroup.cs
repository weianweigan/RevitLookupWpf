using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class DefinitionGroupInstanceNode : InstanceNode<DefinitionGroup>
    {
        public DefinitionGroupInstanceNode(DefinitionGroup rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Name})";
            }
        }
    }
}
