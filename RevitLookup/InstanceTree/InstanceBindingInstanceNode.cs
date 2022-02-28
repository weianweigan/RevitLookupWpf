using System.Text;
using Autodesk.Revit.DB;

namespace RevitLookupWpf.InstanceTree
{
    public class InstanceBindingInstanceNode : InstanceNode<InstanceBinding>
    {
        public InstanceBindingInstanceNode(InstanceBinding rvtObject) : base(rvtObject)
        {
            if (rvtObject != null)
            {
                StringBuilder sb = new StringBuilder();
                int index = 0;
                foreach (Category cat in rvtObject.Categories)
                {
                    if(rvtObject.Categories.Size==1)sb.Append($"({cat.Name})");
                    else if (index == 0) sb.Append($"({cat.Name}");
                    else if (index==rvtObject.Categories.Size) sb.Append($"{cat.Name})");
                    else sb.Append($",{cat.Name}");
                    index++;
                }
                Name += sb.ToString();
            }
        }
    }
}
