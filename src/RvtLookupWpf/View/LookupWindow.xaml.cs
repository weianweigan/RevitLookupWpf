using Autodesk.Revit.DB;
using RvtLookupWpf.ViewModel;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RvtLookupWpf.View
{
    /// <summary>
    /// Interaction logic for LookupWindow.xaml
    /// </summary>
    public partial class LookupWindow : Window
    {
        private LookupWindowViewModel _viewModel = new LookupWindowViewModel();

        public LookupWindow()
        {
            InitializeComponent();
            _viewModel.CloseAction = new Action(() => Close());
            DataContext = _viewModel;
        }

        public LookupWindow(IntPtr parentHandle)
        {
            InitializeComponent();

            //设置父窗口为Revit
            var windowInteropHelper = new WindowInteropHelper(this);
            windowInteropHelper.Owner = parentHandle;

            _viewModel.CloseAction = new Action(() => Close());
            DataContext = _viewModel;
        }

        public bool SetRvtInstance<TRvtObject>(TRvtObject rvtObject)
        {
            return _viewModel.SetRvtInstance(rvtObject);
        }
    }
}
