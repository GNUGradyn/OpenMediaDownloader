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
        private string _formatId;
        private bool _useVideo = false;
        private bool _useAudio = false;
        private string _height;
        private string _width;
        private string _container;
        private string _audioCodec;
        private string _videoCodec;
        private string _fps;
        private string _videoCodecDescription;
        private string _audioCodecDescription;
        

        public string FormatId
        {
            get
            {
                return _formatId;
            }
            set
            {
                _formatId = value;
                // No need to call OnPropertyChanged as this has no UI bindings
            }
        }

        public string VideoCodecDescription
        {
            get
            {
                return _videoCodecDescription;
            }
            set
            {
                _videoCodecDescription = value;
                OnPropertyChanged(nameof(VideoCodecDescription));
            }
        }
        
        public string AudioCodecDescription
        {
            get
            {
                return _audioCodecDescription;
            }
            set
            {
                _audioCodecDescription = value;
                OnPropertyChanged(nameof(AudioCodecDescription));
            }
        }
        
        public bool UseVideo
        {
            get { return _useVideo; }
            set
            {
                if (_useVideo != value) // This check is neccesary to prevent an infinite loop where the event handler for updating UseVideo triggers itself
                {
                    _useVideo = value;
                    OnPropertyChanged(nameof(UseVideo));
                }
            }
        }

        public bool UseAudio
        {
            get { return _useAudio; } 
            set
            {
                if (_useAudio != value) // This check is neccesary to prevent an infinite loop where the event handler for updating UseAudio triggers itself
                {
                    _useAudio = value;
                    OnPropertyChanged(nameof(UseAudio));
                }
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
