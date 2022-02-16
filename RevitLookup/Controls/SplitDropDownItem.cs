using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitLookupWpf.Controls
{
    /// <summary>
    ///<MyNamespace:SplitDropDownItem/>
    /// </summary>
    public class SplitDropDownItem : Control
    {
        static SplitDropDownItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitDropDownItem), new FrameworkPropertyMetadata(typeof(SplitDropDownItem)));
        }

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(SplitDropDownItem), new PropertyMetadata(null));

        /// <summary>
        /// Text to display Method or Property name
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SplitDropDownItem), new PropertyMetadata(null));

        public ICommand ApplyCommand
        {
            get { return (ICommand)GetValue(ApplyCommandProperty); }
            set { SetValue(ApplyCommandProperty, value); }
        }

        public static readonly DependencyProperty ApplyCommandProperty =
            DependencyProperty.Register("ApplyCommand", typeof(ICommand), typeof(SplitDropDownItem), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var btn = GetTemplateChild("PART_Btn") as Button;
            btn.Click += Btn_Click;

            var aplyBtn = GetTemplateChild("PART_ApplyBtn") as Button;
            aplyBtn.Click += AplyBtn_Click;
        }

        private void AplyBtn_Click(object sender, RoutedEventArgs e)
        {
            var popUp = GetTemplateChild("PART_Popup") as Popup;

            popUp.IsOpen = false;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {            
            var popUp = GetTemplateChild("PART_Popup") as Popup;

            popUp.IsOpen = !popUp.IsOpen;
        }
    }
}
