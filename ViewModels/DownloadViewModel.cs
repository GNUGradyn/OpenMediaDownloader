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
        private readonly string _path;
        
        public DownloadViewModel(string name, BitmapImage thumbnail, string path)
        {
            _name = name;
            _thumbnail = thumbnail;
            _path = path;
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
        public string Path => _path;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}