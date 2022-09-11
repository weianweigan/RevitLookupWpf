using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Command;
using RevitLookupWpf.GeometryConverter;
using RevitLookupWpf.GeometryTree;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.ViewModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.View
{
    /// <summary>
    /// PreviewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PreviewWindow : Window
    {
        private RelayCommand zoomFitCommand;

        public PreviewWindow()
        {
            InitializeComponent();
            DataContext = this;
            this.SetOwnerWindow();
        }

        public List<RootGeometryNode> RootGeometryNode { get; set; }
            = new List<RootGeometryNode>(1)
        {
            new RootGeometryNode()
        };

        public RelayCommand ZoomFitCommand { get => zoomFitCommand ??= new RelayCommand(ZoomFitClick); }

        private void ZoomFitClick()
        {
            ThreeDView.ZoomExtents(1000);
        }

        internal void AddGeometryNode(GeometryNode geometryNode)
        {
            RootGeometryNode.First().Children.Add(geometryNode);

            var visual = geometryNode.LoadModel3D();

            if (visual != null)
            {
                ThreeDView.Children.Add(visual);
            }
        }
    }
}
