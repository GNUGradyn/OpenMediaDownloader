using OpenMediaDownloader.Models;
using OpenMediaDownloader.ViewModels;
using OpenMediaDownloader.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

            var viewModel = new DownloadWindowViewModel()
            {
                Title = metadata.Title,
                Thumbnail = bitmap,
                Resolution = $"{metadata.Width}x{metadata.Height}",
                Duration = metadata.DurationString,
                FPS = metadata.FPS,
                Container = metadata.Container,
                VideoCodec = metadata.VideoCodec,
                AudioCodec = metadata.AudioCodec,
                Uploader = metadata.Uploader,
                OutputFormatViewModels = GetOutputFormatModelFromFormatOptions(new ObservableCollection<Models.FormatOption>(metadata.Formats.Where(x => x.Container != "mhtml").Reverse()))
            };

            setDefaultVideoAndAudioStream(ref viewModel);

            new DownloadWindow()
            {
                DataContext = viewModel
            }.Show();
        }

        public ObservableCollection<OutputFormatViewModel> GetOutputFormatModelFromFormatOptions(ObservableCollection<Models.FormatOption> value)
        {
            var formatOptionObservableCollection = (value as ObservableCollection<FormatOption>);
            return new ObservableCollection<OutputFormatViewModel>(formatOptionObservableCollection.ToList().Select(x => new OutputFormatViewModel
            {
                UseVideo = x.UseVideo,
                UseAudio = x.UseAudio,
                Height = x.Height.ToString() ?? "N/A",
                Width = x.Width.ToString() ?? "N/A",
                Container = string.IsNullOrEmpty(x.Container) ? "Unknown" : x.Container,
                AudioCodec = GetAudioCodecName(x.AudioCodec),
                VideoCodec = GetVideoCodecName(x.VideoCodec),
                FPS = x.FPS.ToString() ?? "Unknown",
            }));
        }

        private string GetAudioCodecName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Unknown";
            if (name == "none") return "Video Only";
            return name;
        }

        private string GetVideoCodecName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Unknown";
            if (name == "none") return "Audio Only";
            return name;
        }

        private void setDefaultVideoAndAudioStream(ref DownloadWindowViewModel viewModel)
        {
            bool shouldSelectVideo = viewModel.OutputFormatViewModels.Any(x => !string.IsNullOrEmpty(x.VideoCodec) && x.VideoCodec != "Audio Only");
            if (shouldSelectVideo) // We need to select a default video stream
            {
                viewModel.OutputFormatViewModels.Where(x => !string.IsNullOrEmpty(x.VideoCodec) && x.VideoCodec != "Video Only").First().UseVideo = true;
            }

            if (viewModel.OutputFormatViewModels.Any(x => !string.IsNullOrEmpty(x.AudioCodec) && x.AudioCodec != "Video Only"))
            { // We need to select a default audio stream

                if (shouldSelectVideo) // We are also selecting video. Give preference to the stream we are already downloading so we dont have to mix the audio and video together
                {
                    var defaultVideoStream = viewModel.OutputFormatViewModels.Where(x => !string.IsNullOrEmpty(x.VideoCodec) && x.VideoCodec != "Audio Only").First();
                    if (!string.IsNullOrEmpty(defaultVideoStream.AudioCodec) && defaultVideoStream.AudioCodec != "Video Only") // Video already has audio. Simply use the same file
                    {
                        defaultVideoStream.UseAudio = true;
                        return;
                    }
                }
                // If we've gotten this far, we're either working with an audio only download or the default video did not have audio
                if (viewModel.OutputFormatViewModels.Any(x => (string.IsNullOrEmpty(x.VideoCodec) || x.VideoCodec == "Audio Only") && (!string.IsNullOrEmpty(x.AudioCodec) && x.AudioCodec != "Video Only"))) // Give preference to audio only files for audio
                {
                    viewModel.OutputFormatViewModels.First(x => (string.IsNullOrEmpty(x.VideoCodec) || x.VideoCodec == "Audio Only") && (!string.IsNullOrEmpty(x.AudioCodec) && x.AudioCodec != "Video Only")).UseAudio = true;
                    return;
                }
                // If we've gotten this far there were no audio only files to choose from but we still need to select a default audio stream. Just go with whatever we can at this point.
                viewModel.OutputFormatViewModels.Where(x => !string.IsNullOrEmpty(x.AudioCodec) && x.AudioCodec == "Video Only").First().UseAudio = true;
            }
        }

        private string RemoveQueryParameters(string url)
        {
            var uri = new Uri(url);
            return uri.GetLeftPart(UriPartial.Path);
        }
    }
}
