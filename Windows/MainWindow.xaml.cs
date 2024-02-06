using OpenMediaDownloader.ViewModels;
using OpenMediaDownloader.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace OpenMediaDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IYoutubeDlService _youtubeDlService;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            _youtubeDlService = new YoutubeDlService();
        }

        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as MainWindowViewModel;
            if (string.IsNullOrWhiteSpace(context.SearchText)) return;
            context.Loading = true;
            Video metadata = await _youtubeDlService.GetVideoMetadata(context.SearchText);
            if (metadata == null)
            {
                context.Loading = false;
                MessageBox.Show("Unable to fetch metadata for the requested video. Please check the URL", "Open Media Downloader", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
            }

            // Download thumbnail
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(RemoveQueryParameters(metadata.Thumbnail), UriKind.Absolute);
            bitmap.EndInit();
            context.Loading = false;

            new DownloadWindow()
            {
                DataContext = new DownloadWindowViewModel()
                {
                    Title = metadata.Title,
                    Thumbnail = bitmap,
                    OriginalHeight = metadata.Hegiht,
                    OriginalWidth = metadata.Width
                }
            }.Show();
        }

        private string RemoveQueryParameters(string url)
        {
            var uri = new Uri(url);
            return uri.GetLeftPart(UriPartial.Path);
        }
    }
}
