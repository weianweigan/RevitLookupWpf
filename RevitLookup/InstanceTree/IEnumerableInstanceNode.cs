using System.Collections;
using System.Collections.ObjectModel;
using RevitLookupWpf.Helpers;

namespace RevitLookupWpf.InstanceTree
{
    public class IEnumerableInstanceNode : InstanceNode<IEnumerable>
    {
        public IEnumerableInstanceNode(IEnumerable rvtObject) : base(rvtObject)
        {
            GetChild();
            Name = RvtObject?.GetType().Name + $" [{Children.Count}]";
        }

        private void GetChild()
        {
            if (Children == null)
            {
                Children = new ObservableCollection<InstanceNode>();
            }

            Index = -1;
            foreach (object item in RvtObject)
            {
                InstanceNode node = InstanceNode.Create(item);
                Children.Add(node);
            }

            if (Children.Any())
            {
                Children = Children.OrderBy(x => x.Name).ToObservableCollection();
                Children.ToList().ForEach(x=>x.Index= Index+=1);
                Children.ToList().ForEach(x=>x.Name= $"[{x.Index}] {x.Name}");
            }
        }

    }
}
