using System.ComponentModel;
using System.Runtime.CompilerServices;
using OpenMediaDownloader.Models;

namespace OpenMediaDownloader.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {

        public DownloadViewModel(OutputFormatViewModel outputFormat)
        {
            _outputFormat = outputFormat;
            _status = 0;
        }
        
        private float _status;
        private OutputFormatViewModel _outputFormat;

        public float Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public OutputFormatViewModel OutputFormat => _outputFormat; // This will not change, no setter neccesary

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}