using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using OpenMediaDownloader.Models;

namespace OpenMediaDownloader.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private readonly string _title;
        private readonly BitmapImage _thumbnail;
        private float _progress;
        private readonly string _path;
        
        public DownloadViewModel(string title, BitmapImage thumbnail, string path)
        {
            _title = title;
            _thumbnail = thumbnail;
            _path = path;
        }

        public float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        // These do not need setters. they will not change after being set by the constructor
        public string Title => _title;
        public BitmapImage Thumbnail => _thumbnail;
        public string Path => _path;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}