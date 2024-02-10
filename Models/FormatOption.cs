using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenMediaDownloader.Models
{
    public class FormatOption : INotifyPropertyChanged
    {
        private bool _useVideo = false;
        private bool _useAudio = false;

        [JsonProperty("height")]
        public int? Height { get; set; }
        [JsonProperty("width")]
        public int? Width { get; set; }
        [JsonProperty("ext")]
        public string Container { get; set; }
        [JsonProperty("acodec")]
        public string AudioCodec { get; set; }
        [JsonProperty("vcodec")] 
        public string VideoCodec { get; set; }
        [JsonProperty("fps")]
        public float? FPS { get; set; }

        public bool UseVideo { 
            get
            {
                return _useVideo;
            }
            set { 
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}