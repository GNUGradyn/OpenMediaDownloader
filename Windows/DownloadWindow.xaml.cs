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

        public void DataGridLoaded(object sender, RoutedEventArgs e)
        {
            setDefaults();
        }

        private void setDefaults()
        {
            DownloadWindowViewModel viewModel = DataContext as DownloadWindowViewModel;
            bool shouldSelectVideo = viewModel.FormatOptions.Any(x => !string.IsNullOrEmpty(x.VideoCodec) && x.VideoCodec != "none");
            if (shouldSelectVideo) // We need to select a default video stream
            {
                viewModel.FormatOptions.Where(x => !string.IsNullOrEmpty(x.VideoCodec) && x.VideoCodec != "none").First().UseVideo = true;
            }

            if (viewModel.FormatOptions.Any(x => !string.IsNullOrEmpty(x.AudioCodec) && x.AudioCodec != "none"))
            { // We need to select a default audio stream

                if (shouldSelectVideo) // We are also selecting video. Give preference to the stream we are already downloading so we dont have to mix the audio and video together
                {
                    var defaultVideoStream = viewModel.FormatOptions.Where(x => !string.IsNullOrEmpty(x.VideoCodec) && x.VideoCodec != "none").First();
                    if (!string.IsNullOrEmpty(defaultVideoStream.AudioCodec) && defaultVideoStream.AudioCodec != "none")
                    {
                        defaultVideoStream.UseAudio = true;
                        return;
                    }
                }
                // If we've gotten this far we are either doing an audio only download or the default video stream does not have an assosiated audio stream
                viewModel.FormatOptions.Where(x => !string.IsNullOrEmpty(x.AudioCodec) && x.AudioCodec != "none").First().UseAudio = true;
            }
        }
    }
}
