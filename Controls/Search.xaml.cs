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
            DependencyProperty.Register("SearchText", typeof(string), typeof(Search), new PropertyMetadata("Paste link here"));

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty LoadingProperty =
            DependencyProperty.Register("Loading", typeof(bool), typeof(Search), new PropertyMetadata(default(bool)));

        public bool Loading
        {
            get { return (bool)GetValue(LoadingProperty); }
            set { SetValue(LoadingProperty, value); }
        }

        public string SearchQueryColor
        {
            get { return _searchQueryColor; }
            set { 
                _searchQueryColor = value;
                UpdateSearchQueryColorInUI(_searchQueryColor);
            }
        }

        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchText == "Paste link here")
            {
                SearchText = string.Empty;
                SearchQueryColor = "#000000";
            }
        }

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                SearchText = "Paste link here";
                SearchQueryColor = "#b3b3b3";
            }
        }

        private void UpdateSearchQueryColorInUI(string color)
        {
            var colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(color);
            searchTextBox.Foreground = colorBrush;
        }
    }
}
