using System.Collections;
using System.Collections.ObjectModel;
using RevitLookupWpf.Helpers;

namespace RevitLookupWpf.InstanceTree
{
    public class IEnumerableInstanceNode : InstanceNode<IEnumerable>
    {
        public IEnumerableInstanceNode(IEnumerable rvtObject) : base(rvtObject)
        {
            Name = RvtObject?.GetType().Name;
            GetChild();
        }

        private void GetChild()
        {
            if (Children == null)
            {
                Children = new ObservableCollection<InstanceNode>();
            }
            foreach (var item in RvtObject)
            {
                InstanceNode node = InstanceNode.Create(item);
                Children.Add(node);
            }
            if (Children.Any()) Children = Children.OrderBy(x => x.Name).ToObservableCollection();
        }

    }
}
