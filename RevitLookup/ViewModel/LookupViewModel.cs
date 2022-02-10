using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using RevitLookupWpf.InstanceTree;
using RevitLookupWpf.PropertySys;

namespace RevitLookupWpf.ViewModel
{
    public class LookupViewModel:ViewModelBase
    {

        private PropertyList _propertyList;
        private ListCollectionView _dataSource;
        private ObservableCollection<InstanceNode> _roots;
        private LookupViewModel _lookupData;

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
            if (LookupData?.Roots == null)
            {
                return null;
            }

            foreach (var root in LookupData.Roots)
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

                if (_propertyList != null)
                {
                    DataSource = new ListCollectionView(_propertyList);
                    DataSource.SortDescriptions.Add(new SortDescription("Category",ListSortDirection.Descending));
                    DataSource.SortDescriptions.Add(new SortDescription("Name",ListSortDirection.Ascending));
                    DataSource.GroupDescriptions?.Add(new PropertyGroupDescription("Category"));
                }
            }
        }

        public ListCollectionView DataSource
        {
            get { return _dataSource; }
            set
            {

                _dataSource = value;
                RaisePropertyChanged(nameof(DataSource));
            }
        }

        public LookupViewModel Next { get; set; }

        public string Name { get; set; }

        public string NaviName { get; set; }

        public LookupViewModel LookupData
        {
            get => _lookupData; set
            {
                Set(ref _lookupData, value);
                RaisePropertyChanged(() => LookupData.DataSource);
            }
        }
    }
}
