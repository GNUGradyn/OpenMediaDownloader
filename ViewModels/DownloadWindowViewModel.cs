using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OpenMediaDownloader.ViewModels
{
    internal class DownloadWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private BitmapImage _thumbnail;
        private string _resolution;
        private string _container;
        private string _duration;
        private float _fps;
        private string _videoCodec;
        private string _audioCodec;
        private string _uploader;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Resolution
        {
            get
            {
                return _resolution;
            }
            set
            {
                _resolution = value;
                OnPropertyChanged(nameof(Resolution));
            }
        }

        public string Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
                OnPropertyChanged(nameof(Container));
            }
        }

        public string Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public float FPS
        {
            get
            {
                return _fps;
            }
            set
            {
                _fps = value;
                OnPropertyChanged(nameof(FPS));
            }
        }

        public string VideoCodec
        {
            get
            {
                return _videoCodec;
            }
            set
            {
                _videoCodec = value;
                OnPropertyChanged(nameof(VideoCodec));
            }
        }

        public string AudioCodec
        {
            get
            {
                return _audioCodec;
            }
            set
            {
                _audioCodec = value;
                OnPropertyChanged(nameof(AudioCodec));
            }
        }

        public string Uploader
        {
            get
            {
                return _uploader;
            }
            set
            {
                _uploader = value;
                OnPropertyChanged(nameof(Uploader));
            }
        }

        public BitmapImage Thumbnail
        {
            get
            {
                return _thumbnail;
            }
            set
            {
                _thumbnail = value;
                OnPropertyChanged(nameof(Thumbnail));
            }
        }



        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
