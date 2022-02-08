using System.Windows;
using System.Windows.Controls;
using RevitLookup.ViewModel;

namespace RevitLookup.Controls
{
    public class BreadCrumNavigation:StackPanel
    {
        public LookupViewModel RootContext
        {
            get { return (LookupViewModel)GetValue(RootContextProperty); }
            set { SetValue(RootContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RootContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RootContextProperty =
            DependencyProperty.Register("RootContext", typeof(LookupViewModel), typeof(BreadCrumNavigation), new PropertyMetadata(null,RootContextChangedCallback));

        private static void RootContextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bread = (BreadCrumNavigation)d;
            bread.OnRootContextChanged((LookupViewModel)e.OldValue, (LookupViewModel)e.NewValue);
        }

        private void OnRootContextChanged(LookupViewModel oldValue, LookupViewModel newValue)
        {
            if (newValue != null)
            {
                Children.Clear();
            }
            else if(oldValue != newValue)
            {
                var current = newValue;
                while (current != null)
                {
                    DataTemplate data;
                   
                    Children.Add(new Button()
                    {
                        Content = current.Name
                    });

                    current = current.Next;
                }
            }
        }

        public LookupViewModel SelectedContext
        {
            get { return (LookupViewModel)GetValue(SelectedContextProperty); }
            set { SetValue(SelectedContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedContextProperty =
            DependencyProperty.Register("SelectedContext", typeof(LookupViewModel), typeof(BreadCrumNavigation), new PropertyMetadata(null,SelectedContextChangedCallback));

        private static void SelectedContextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
