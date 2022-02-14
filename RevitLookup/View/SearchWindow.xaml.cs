using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RevitLookupWpf.ViewModel;

namespace RevitLookupWpf.View
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        protected SnoopSearchViewModel vm;
        public SearchWindow(SnoopSearchViewModel viewModel)
        {
            InitializeComponent();
            vm = viewModel;
            DataContext = viewModel;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            vm.SearchWindow = this;
        }
    }
}
