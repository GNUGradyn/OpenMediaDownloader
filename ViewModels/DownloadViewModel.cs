using System.ComponentModel;
using System.Runtime.CompilerServices;
using OpenMediaDownloader.Models;

namespace OpenMediaDownloader.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private float _status;
        private OutputFormatViewModel _videoFormat;
        private OutputFormatViewModel _audioFormat;
        
        public DownloadViewModel(OutputFormatViewModel videoFormat, OutputFormatViewModel audioFormat)
        {
            _videoFormat = videoFormat;
            _audioFormat = audioFormat;
            _status = 0;
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

        public OutputFormatViewModel VideoFormat => _videoFormat; // This will not change, no setter neccesary
        public OutputFormatViewModel AudioFormat => _audioFormat; // This will not change, no setter neccesary
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}