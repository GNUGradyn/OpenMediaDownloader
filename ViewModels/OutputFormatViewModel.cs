using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenMediaDownloader.Models
{
    public class OutputFormatViewModel : INotifyPropertyChanged
    {
        private bool _useVideo = false;
        private bool _useAudio = false;
        private string _height;
        private string _width;
        public string _container;
        public string _audioCodec;
        public string _videoCodec;
        private string _fps;

        public bool UseVideo
        {
            get
            {
                return _useVideo;
            }
            set
            {
                _useVideo = value;
                OnPropertyChanged(nameof(UseVideo));
            }
        }
        public bool UseAudio
        {
            get
            {
                return _useAudio;
            }
            set
            {
                _useAudio = value;
                OnPropertyChanged(nameof(UseAudio));
            }
        }

        public string Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public string Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
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
        public string FPS
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
