using Autodesk.Revit.DB;
using RevitLookupWpf.GeometryConverter;
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
        public PreviewWindow()
        {
            InitializeComponent();
            DataContext = (ViewModel = new PreviewWindowViewModel());
            this.SetOwnerWindow();
        }

        public PreviewWindowViewModel ViewModel { get; }

        internal void AddSolid(Solid solid)
        {
            var geo = solid.ToGeometry3D();
            var geometryModel = new GeometryModel3D()
            {
                Geometry = geo,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Gray)),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray)),
            };

            var modelGroup = new Model3DGroup();
            modelGroup.Children.Add(geometryModel);

            var visual = new ModelVisual3D()
            {
                Content = modelGroup
            };

            ThreeDView.Children.Add(visual);

            ThreeDView.ZoomExtents();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThreeDView.ZoomExtents(1000);
        }
    }
}
