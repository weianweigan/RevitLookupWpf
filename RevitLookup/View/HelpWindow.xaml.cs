using RevitLookupWpf.Helpers;
using System.Diagnostics;
using System.Windows;

namespace RevitLookupWpf.View
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HelpWindow : Window
    {
        private readonly RevitInfo _revitInfo;

        public HelpWindow(RevitInfo revitInfo)
        {
            _revitInfo = revitInfo;
            InitializeComponent();

            DataContext = _revitInfo;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(
                _revitInfo.GetUrl(RevitInfoManager.Version?.VersionNumber)
                );
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
