using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class TextboxWithPlaceholder : UserControl, INotifyPropertyChanged
    {
        public TextboxWithPlaceholder()
        {
            InitializeComponent();
        }

        private string _searchQueryColor = "#b3b3b3";
        private string _actualText; // This will be the placeholder if the textbox is empty and not selected. This allows the TextProperty to always represent the users input

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextboxWithPlaceholder), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextboxWithPlaceholder), new PropertyMetadata(string.Empty, SetTextColor));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private static void SetTextColor(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextboxWithPlaceholder obj = d as TextboxWithPlaceholder;
            if (string.IsNullOrEmpty(obj.Text))
            {
                obj.SearchQueryColor = "#b3b3b3";
            }
            else
            {
                obj.SearchQueryColor = "#000000";
            }
        }

        public string ActualText
        {
            get
            {
                return _actualText;
            }
            set
            {
                _actualText = value;
                if (value != Placeholder) Text = value;
                OnPropertyChanged(nameof(ActualText));
            }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { 
                SetValue(PlaceholderProperty, value);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { 
                SetValue(TextProperty, value);

            }
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
            ActualText = Placeholder;
        }

        private void TextboxGotFocus(object sender, RoutedEventArgs e)
        {
                ActualText = Text;
        }

        private void TextboxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                ActualText = Placeholder;
            }
        }

        private void UpdateSearchQueryColorInUI(string color)
        {
            var colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(color);
            textbox.Foreground = colorBrush;
        }
    }
}
