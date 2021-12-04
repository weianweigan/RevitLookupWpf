
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Input;
using RvtLookupWpf.PropertySys;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Autodesk.Revit.DB;

namespace RvtLookupWpf.ViewModel
{
    public class LookupWindowViewModel : LookupViewModel
    {
        #region Fields
        private string _title = "LookupWindow";
        private RelayCommand _closeCommand;

        private RelayCommand _selectedItemChangedCommand;
        private LookupViewModel _lookupData;
        #endregion

        #region Ctor

        public LookupWindowViewModel()
        {
            LookupData = this;
        }

        #endregion

        #region Properties
        public string Title { get => _title; set => Set(ref _title, value); }

        public Action CloseAction { get; set; }

        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(CloseAction));

        public LookupViewModel LookupData { get => _lookupData; set => Set(ref _lookupData,value); }
        #endregion

        #region Public Methods
        public bool SetRvtInstance<TRvtObject>(TRvtObject rvtObject)
        {
            var root = InstanceNode.Create<TRvtObject>(rvtObject);
            root.IsSelected = true;

            LookupData.Roots = new ObservableCollection<InstanceNode>() { root };

            LookupData.PropertyList = GetSelectedNode()?.PropertyList;

            return LookupData.Roots.Any();
        }

        public ICommand SelectedItemChangedCommand
        {
            get
            {
                if (_selectedItemChangedCommand == null)
                {
                    _selectedItemChangedCommand = new RelayCommand(PerformSelectedItemChanged);
                }

                return _selectedItemChangedCommand;
            }
        }

        private void PerformSelectedItemChanged()
        {
            if (LookupData.Roots == null)
            {
                return;
            }

            PropertyList = GetSelectedNode()?.PropertyList;
        }

        private InstanceNode GetSelectedNode()
        {
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
        #endregion
    }
}
