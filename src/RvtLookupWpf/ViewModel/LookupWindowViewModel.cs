
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
using GalaSoft.MvvmLight.Messaging;

namespace RvtLookupWpf.ViewModel
{
    public class LookupWindowViewModel : LookupViewModel
    {
        #region Fields
        private string _title = "LookupWindow";
        private RelayCommand _closeCommand;

        private RelayCommand _selectedItemChangedCommand;
        private LookupViewModel _lookupData;
        private List<LookupViewModel> _items;
        #endregion

        #region Ctor

        public LookupWindowViewModel()
        {
            LookupData = this;
            Items = GetAllSnoopItems().ToList();

            Messenger.Default.Register<RvtObjectMessage>(this, OnNavigation);
        }
        #endregion

        #region Properties
        public string Title { get => _title; set => Set(ref _title, value); }

        public Action CloseAction { get; set; }

        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(CloseAction));

        public LookupViewModel LookupData
        {
            get => _lookupData; set
            {
                Set(ref _lookupData, value);
                RaisePropertyChanged(() => LookupData.DataSource);
            }
        }

        public List<LookupViewModel> Items { get => _items; set => Set(ref _items , value); }
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

        private IEnumerable<LookupViewModel> GetAllSnoopItems()
        {
            LookupViewModel current = this;
            yield return current;
            while (current.Next != null)
            {
                current = current.Next;
                yield return current;
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

        private void OnNavigation(RvtObjectMessage objectMessage)
        {
            var vm = new LookupViewModel();

            var root = InstanceNode.Create(objectMessage.RvtObject);
            root.IsSelected = true;

            vm.Roots = new ObservableCollection<InstanceNode>() { root };

            vm.PropertyList = vm.GetSelectedNode()?.PropertyList;

            //导航到下一个对象
            if (vm.Roots.Any())
            {
                this.Next = vm;
                LookupData = vm;
                Items = GetAllSnoopItems().ToList();
            }
        }

        #endregion
    }
}
