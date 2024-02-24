using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenMediaDownloader.Controls
{
    public partial class Search : UserControl
    {
        public Search()
        {
            InitializeComponent();
        }

        private string _searchQueryColor = "#b3b3b3";

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(Search), new PropertyMetadata(string.Empty));

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty LoadingProperty =
            DependencyProperty.Register("Loading", typeof(bool), typeof(Search), new PropertyMetadata(false, OnLoadingChanged));

        private static void OnLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var search = d as Search;
            if (search != null)
            {
                bool loading = (bool)e.NewValue;
                search.UpdateLoadingStatusInUI(loading);
            }
        }

        public bool Loading
        {
            get { return (bool)GetValue(LoadingProperty); }
            set { SetValue(LoadingProperty, value); }
        }

        private void UpdateLoadingStatusInUI(bool loading)
        {
            loader.Visibility = loading ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
