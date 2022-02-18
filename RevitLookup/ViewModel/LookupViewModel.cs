using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.InstanceTree;
using RevitLookupWpf.PropertySys;
using RevitLookupWpf.PropertySys.BaseProperty;
using RevitLookupWpf.PropertySys.BaseProperty.MethodType;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.View;

namespace RevitLookupWpf.ViewModel
{
    public class LookupViewModel : ViewModelBase
    {
        private PropertyList _propertyList;
        private ListCollectionView _dataSource;
        private ObservableCollection<InstanceNode> _roots;
        protected LookupViewModel _lookupData;
        private PropertyBase _selectedProperty;
        private RelayCommand _openInNewWindowCommand;
        private RelayCommand _helpCommand;
        public LookupWindow _lookupWindow;
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
            }
        }

        public RelayCommand OpenInNewWindowCommand => _openInNewWindowCommand ?? new RelayCommand(OpenInNewWindow, CanOpenInNewWindow);
        public RelayCommand HelpCommand => _helpCommand ?? new RelayCommand(HelpClick);

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
                helpWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error",ex.Message);
            }
        }

        void GotoSearchOnline()
        {
            string query = $"https://www.revitapidocs.com/2022/?query={SelectedProperty.Name}";
            Process.Start(query);
        }

        private void OpenInNewWindow()
        {
            var lookupWindow = new LookupWindow();
            if (SelectedProperty is DefaultObjectProperty objectProperty)
            {
                lookupWindow.SetRvtInstance(objectProperty.Value);
            }
            else if (SelectedProperty is MethodProperty methodProperty)
            {
                lookupWindow.SetRvtInstance(methodProperty.MethodValue);
            }else if(SelectedProperty is ParametersProperty parametersProperty)
            {
                lookupWindow.SetRvtInstance(parametersProperty.Value);
            }    
            lookupWindow.Show();
        }

        private bool CanOpenInNewWindow()
        {
            if (SelectedProperty is DefaultObjectProperty objectProperty)
            {
                return objectProperty.Value != null && !objectProperty.IsReadOnly;
            }
            if(SelectedProperty is MethodProperty methodProperty)
            {
                return methodProperty.MethodValue != null && methodProperty.CanExecute;
            }

            if (SelectedProperty is ParametersProperty parametersProperty)
            {
                return parametersProperty.Value != null && !parametersProperty.IsReturnValueType;
            }

            return false;
        }

        public LookupViewModel LookupData
        {
            get => _lookupData; set
            {
                Set(ref _lookupData, value);
                RaisePropertyChanged(() => LookupData.DataSource);
                RaisePropertyChanged(() => LookupData.OpenInNewWindowCommand);
                //Remove back items
                if (LookupData?.Next !=null)
                {
                    LookupData.Next = null;
                    LookupDataChanged();
                }
            }
        }

        protected virtual void LookupDataChanged()
        {
        }
    }
}
