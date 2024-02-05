using OpenMediaDownloader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
       
        public Search()
        {
            InitializeComponent();
            DataContext = new SearchViewModel();
        }

        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            var viewModel = (SearchViewModel)DataContext;
            if (viewModel.SearchText == "Paste link here")
            {
                viewModel.SearchText = string.Empty;
                viewModel.SearchQueryColor = "#000000";
            }
        }

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            var viewModel = (SearchViewModel)DataContext;
            if (viewModel.SearchText == string.Empty)
            {
                viewModel.SearchText = "Paste link here";
                viewModel.SearchQueryColor = "#b3b3b3";
            }
        }
    }
}
