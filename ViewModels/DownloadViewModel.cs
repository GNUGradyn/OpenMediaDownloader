using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using OpenMediaDownloader.Models;

namespace OpenMediaDownloader.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private readonly string _name;
        private readonly BitmapImage _thumbnail;
        private float _status;
        private readonly string _videoFormatInfo;
        private readonly string _audioFormatInfo;
        
        public DownloadViewModel(string name, BitmapImage thumbnail, string videoFormatInfo, string audioFormatInfo)
        {
            _name = name;
            _thumbnail = thumbnail;
            _videoFormatInfo = videoFormatInfo;
            _audioFormatInfo = audioFormatInfo;
        }

        public float Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        // These do not need setters. they will not change after being set by the constructor
        public string Name => _name;
        public BitmapImage Thumbnail => _thumbnail;
        public string VideoFormatInfo => _videoFormatInfo;
        public string AudioFormatInfo => _audioFormatInfo;
        
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}