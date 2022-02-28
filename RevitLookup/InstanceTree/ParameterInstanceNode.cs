using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class ParameterInstanceNode : InstanceNode<Parameter>
    {
        public ParameterInstanceNode(Parameter rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                Name += $"({rvtObject.Definition.Name})";
            }
        }
    }
}
