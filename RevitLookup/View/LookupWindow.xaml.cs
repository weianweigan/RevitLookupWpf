using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using Autodesk.Revit.UI;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.ViewModel;

namespace RevitLookupWpf.View
{
    /// <summary>
    /// Interaction logic for LookupWindow.xaml
    /// </summary>
    public partial class LookupWindow : Window
    {
        private LookupWindowViewModel _viewModel;

        public LookupWindow()
        {
            InitializeComponent();
            this.SetOwnerWindow();
            this.Closed += LookupWindow_Closed;
        }
        public LookupWindow(ExternalCommandData data) : this()
        {
            _viewModel = new LookupWindowViewModel(this);
            _viewModel.CloseAction = Close;
            DataContext = _viewModel;
        }

        public bool SetRvtInstance<TRvtObject>(TRvtObject rvtObject)
        {
            return _viewModel.SetRvtInstance(rvtObject);
        }
        private void LookupWindow_Closed(object sender, EventArgs e)
        {
            this.Closed -= LookupWindow_Closed;

            //TODO:Fix this
            //Messenger.Default.Unregister(_viewModel);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _viewModel.SelectedItemChangedCommand?.Execute(null);
        }

        private void LookupWindow_OnClosing(object sender, CancelEventArgs e)
        {
            ProcessManager.SetActivateWindow();
        }
    }
}
