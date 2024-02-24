using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenMediaDownloader.Controls
{
    /// <summary>
    /// Interaction logic for TextboxWithPlaceholder.xaml
    /// </summary>
    public partial class TextboxWithPlaceholder : UserControl
    {
        public TextboxWithPlaceholder()
        {
            InitializeComponent();
        }

        private string _searchQueryColor = "#b3b3b3";

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextboxWithPlaceholder), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextboxWithPlaceholder), new PropertyMetadata(string.Empty));



        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string SearchQueryColor
        {
            get { return _searchQueryColor; }
            set
            {
                _searchQueryColor = value;
                UpdateSearchQueryColorInUI(_searchQueryColor);
            }
        }

        public void TextboxLoaded(object sender, RoutedEventArgs e)
        {
            Text = Placeholder;
        }

        private void TextboxGotFocus(object sender, RoutedEventArgs e)
        {
            if (Text == Placeholder)
            {
                Text = string.Empty;
                SearchQueryColor = "#000000";
            }
        }

        private void TextboxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                Text = Placeholder;
                SearchQueryColor = "#b3b3b3";
            }
        }

        private void UpdateSearchQueryColorInUI(string color)
        {
            var colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(color);
            textbox.Foreground = colorBrush;
        }
    }
}
