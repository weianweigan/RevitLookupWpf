using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RevitLookupWpf.GeometryConverter;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.PropertySys;
using RevitLookupWpf.PropertySys.BaseProperty;
using RevitLookupWpf.PropertySys.BaseProperty.MethodType;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.PropertySys.BaseProperty.ValueType;
using RevitLookupWpf.View;
using InstanceNode = RevitLookupWpf.InstanceTree.InstanceNode;

namespace RevitLookupWpf.ViewModel
{
    public class LookupViewModel : ViewModelBase
    {
        #region Fields
        private PropertyList _propertyList;
        private ListCollectionView _dataSource;
        private ObservableCollection<InstanceNode> _roots;
        protected LookupViewModel _lookupData;
        private PropertyBase _selectedProperty;
        private RelayCommand _openInNewWindowCommand;
        private RelayCommand _copy;
        private RelayCommand _helpCommand;
        public LookupWindow _lookupWindow;
        private RelayCommand _preViewCommand;
        #endregion

        #region Ctor
        public LookupViewModel(LookupWindow lookupWindow)
        {
            _lookupWindow = lookupWindow;
        }
        #endregion

        #region Properties
        public ObservableCollection<InstanceNode> Roots
        {
            get => _roots; set
            {
                Set(ref _roots, value);
                GetNaviName();
            }
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
                    DataSource.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Descending));
                    DataSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
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
        private ICollectionView itemsView;

        public ICollectionView ItemsView
        {
            get
            {
                if (itemsView == null)
                {
                    itemsView = CollectionViewSource.GetDefaultView(DataSource);
                    itemsView.Filter = filterSearchText;
                }
                return itemsView;
            }
            set => RaisePropertyChanged(nameof(itemsView));
        }
        private bool filterSearchText(dynamic item)
        {
            if (SearchText != null || SearchText != "")
            {
                return SearchText != null && item.Name.ToUpper().Contains(SearchText.ToUpper());
            }
        
            return true;
        }
        private string _searchText;

        public string SearchText
        {
            get
            {
                if (_searchText == null)
                {
                    _searchText = "";
                  
                }
                return _searchText;
            }
            set
            {
                Set(ref _searchText, value);
                RaisePropertyChanged(nameof(SearchText));
                ItemsView.Refresh();
            }
        }

        public LookupViewModel LookupData
        {
            get => _lookupData; set
            {
                LookupDataChanging();
                Set(ref _lookupData, value);
                RaisePropertyChanged("LookupData.DataSource");
                RaisePropertyChanged("LookupData.OpenInNewWindowCommand");
                //Remove back items
                if (LookupData?.Next != null)
                {
                    LookupData.Next = null;
                    LookupDataChanged();
                }
            }
        }

        public LookupViewModel Next { get; set; }

        public string Name { get; set; }

        public string NaviName { get; set; }

        public PropertyBase SelectedProperty
        {
            get => _selectedProperty;
            set
            {
                Set(ref _selectedProperty, value);
                OpenInNewWindowCommand.RaiseCanExecuteChanged();
                PreviewCommand.RaiseCanExecuteChanged();
                if (_selectedProperty != null)
                    UnitConverter.Update(_selectedProperty);
            }
        }

        public RelayCommand OpenInNewWindowCommand => _openInNewWindowCommand ??= new RelayCommand(OpenInNewWindow, CanOpenInNewWindow);
       
        public RelayCommand HelpCommand => _helpCommand ?? new RelayCommand(HelpClick);
        
        public RelayCommand CopyCommand => _copy ?? new RelayCommand(CopyClick);

        public RelayCommand PreviewCommand => _preViewCommand ?? new RelayCommand(PreViewClick, CanPreview);

        public UnitConverterViewModel UnitConverter { get; } = new UnitConverterViewModel();
        #endregion

        #region 
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

        void CopyClick()
        {
            if (SelectedProperty is ExceptionProperty exceptionProperty)
            {
                Clipboard.SetText(exceptionProperty.Value.ToString());
            }
            else if (SelectedProperty is StringProperty stringProperty)
            {
                Clipboard.SetText(stringProperty.Value);
            }
            else if (SelectedProperty is IntProperty intProperty)
            {
                Clipboard.SetText(intProperty.Value.ToString());
            }
            else if (SelectedProperty is DoubleProperty doubleProperty)
            {
                Clipboard.SetText(doubleProperty.Value.ToString());
            }
            else if (SelectedProperty is DefaultObjectProperty objectProperty)
            {
                Clipboard.SetText(objectProperty.Value.ToString());
            }
            else if (SelectedProperty is MethodProperty methodProperty)
            {
                Clipboard.SetText(methodProperty.MethodValue.ToString());
            }
            else if (SelectedProperty is ParametersProperty parametersProperty)
            {
                Clipboard.SetText(parametersProperty.Value.ToString());
            }
        }

        void HelpClick()
        {
            try
            {
                if (SelectedProperty == null) throw new ArgumentException(nameof(SelectedProperty));

                var revitInfo = SelectedProperty.GetRevitInfo();
                if (revitInfo == null)
                {
                    GotoSearchOnline();
                    return;
                }
                var helpWindow = new HelpWindow(revitInfo);
                helpWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                helpWindow.Owner = _lookupWindow;
                helpWindow.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        void GotoSearchOnline()
        {
            string query = $"https://www.revitapidocs.com/2022/?query={SelectedProperty.Name}";
            Process.Start(query);
        }

        private void OpenInNewWindow()
        {
            var lookupWindow = new LookupWindow(SnoopingContext.Instance.CommandData);
            if (SelectedProperty is DefaultObjectProperty objectProperty)
            {
                lookupWindow.SetRvtInstance(objectProperty.Value);
            }
            else if (SelectedProperty is MethodProperty methodProperty)
            {
                if (!methodProperty.SolvedValue)
                {
                    if (!methodProperty.NoParameterSolve())//Invoke to get value when hasn't solved value
                    {
                        TaskDialog.Show("Error", "Cannot open in new window");
                        return;
                    }
                }
                lookupWindow.SetRvtInstance(methodProperty.MethodValue);
            }
            else if (SelectedProperty is ParametersProperty parametersProperty)
            {
                lookupWindow.SetRvtInstance(parametersProperty.Value);
            }
            lookupWindow.Show();
        }

        private bool CanOpenInNewWindow()
        {
            if (SelectedProperty is DefaultObjectProperty objectProperty)
            {
                return objectProperty.Value != null;
            }
            else if (SelectedProperty is MethodProperty methodProperty)
            {
                return methodProperty.MethodValue != null && methodProperty.CanExecute;
            }

            if (SelectedProperty is ParametersProperty parametersProperty)
            {
                return parametersProperty.Value != null && !parametersProperty.IsReturnValueType;
            }

            return false;
        }

        protected virtual void LookupDataChanged()
        {
        }

        private void PreViewClick()
        {
            GeometryPreviewManager.Preview(_selectedProperty);
        }

        private bool CanPreview()
        {
            return SelectedProperty?.IsGeometeryObject == true;
        }

        protected virtual void LookupDataChanging() { }
        #endregion
    }
}
