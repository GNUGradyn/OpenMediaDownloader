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
using System.Windows.Shapes;

namespace OpenMediaDownloader.Windows
{
    /// <summary>
    /// Interaction logic for DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public DownloadWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DownloadWindowViewModel viewModel = DataContext as DownloadWindowViewModel;
            CheckBox checkbox = sender as CheckBox;
            if (checkbox.Name == "UseVideo")
            {
                viewModel.FormatOptions.ToList().ForEach(x => x.UseVideo = false);
            }
            if (checkbox.Name == "UseAudio")
            {
                viewModel.FormatOptions.ToList().ForEach(x => x.UseAudio = false);

            }
        }
    }
}
