using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ParameterInstanceNode : InstanceNode<Parameter>
    {
        public ParameterInstanceNode(Parameter rvtObjcet) : base(rvtObjcet)
        {
            if (rvtObjcet != null)
            {
                Name += $"({rvtObjcet.Definition.Name})";
            }
        }
    }
}
