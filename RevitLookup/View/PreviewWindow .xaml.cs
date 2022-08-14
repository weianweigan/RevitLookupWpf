using Autodesk.Revit.DB;
using HelixToolkit.Wpf.SharpDX;
using RevitLookupWpf.GeometryConverter;
using RevitLookupWpf.Helpers;
using RevitLookupWpf.ViewModel;
using System.Windows;

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

            ThreeDView.Items.Add(new MeshGeometryModel3D()
            {
                Geometry = geo,
            });

            ThreeDView.ZoomExtents();
        }
    }
}
