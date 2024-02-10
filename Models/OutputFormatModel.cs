using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenMediaDownloader.Models
{
    public class OutputFormatModel : INotifyPropertyChanged
    {
        private bool _useVideo = false;
        private bool _useAudio = false;

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

        public string Height { get; set; }
        public string Width { get; set; }
        public string Container { get; set; }
        public string AudioCodec { get; set; }
        public string VideoCodec { get; set; }
        public string FPS { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
