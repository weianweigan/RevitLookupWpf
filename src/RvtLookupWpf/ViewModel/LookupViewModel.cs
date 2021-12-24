using GalaSoft.MvvmLight;
using RvtLookupWpf.PropertySys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RvtLookupWpf.ViewModel
{
    public class LookupViewModel:ViewModelBase
    {

        private PropertyList _propertyList;
        private ListCollectionView _dataSource;
        private ObservableCollection<InstanceNode> _roots;

        public ObservableCollection<InstanceNode> Roots
        {
            get => _roots; set
            {
                Set(ref _roots, value);
                GetNaviName();
            }
        }

        private void GetNaviName()
        {
            if (Roots?.Any() == true)
            {
                NaviName = Roots.First().Name;
                if (Roots.Count > 1)
                {
                    NaviName += "...";
                }
            }
        }

        public InstanceNode GetSelectedNode()
        {
            foreach (var root in Roots)
            {
                if (root.IsSelected)
                    return root;
                foreach (var child in root.RecruChild())
                {
                    if (child.IsSelected)
                    {
                        return child;
                    }
                }
            }
            return null;
        }

        public PropertyList PropertyList
        {
            get => _propertyList; set
            {
                if (value != null && object.ReferenceEquals(_propertyList, value))
                {
                    return;
                }

                Set(ref _propertyList, value);
                DataSource = new ListCollectionView(_propertyList);
            }
        }

        public ListCollectionView DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                _dataSource.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                RaisePropertyChanged(nameof(DataSource));
            }
        }

        public LookupViewModel Next { get; set; }

        public string Name { get; set; }

        public string NaviName { get; set; }
    }
}
