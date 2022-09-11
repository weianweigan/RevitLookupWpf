using RevitLookupWpf.Helpers;
using RevitLookupWpf.ViewModel;
using System.Windows;

namespace RevitLookupWpf.View
{
    /// <summary>
    /// UnitConverterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UnitConverterWindow : Window
    {
        private UnitConverterWindowViewModel _viewModel;

        public UnitConverterWindow(string converterData)
        {
            InitializeComponent();
            _viewModel = new UnitConverterWindowViewModel(converterData)
            {
                CloseAction = ()=> this.Close(),
            };
            DataContext = _viewModel;
            this.SetOwnerWindow();
        }
    }
}
