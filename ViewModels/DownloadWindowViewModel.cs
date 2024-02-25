using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OpenMediaDownloader.Models;

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
        private float? _fps;
        private string _videoCodec;
        private string _audioCodec;
        private string _uploader;
        private ObservableCollection<OutputFormatViewModel> _outputFormatViewModels = new ObservableCollection<OutputFormatViewModel>();
        private string _filename;
        private string _path;

        public ObservableCollection<OutputFormatViewModel> OutputFormatViewModels
        {
            get
            {
                return _outputFormatViewModels;
            }
            set
            {
                _outputFormatViewModels = value;
                OnPropertyChanged(nameof(OutputFormatViewModels));
            }
        }

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

        public string Filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged(nameof(Path));
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

        public float? FPS
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

        public string FileExtension
        {
            get
            {
                if (OutputFormatViewModels.Any(x => x.UseVideo))
                {
                    return "." + OutputFormatViewModels.First(x => x.UseVideo).Container;
                }
                if (OutputFormatViewModels.Any(x => x.UseAudio))
                {
                    return "." + OutputFormatViewModels.First(x => x.UseAudio).Container;
                }
                return "";
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnOutputFormatViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OutputFormatViewModel.UseVideo) || e.PropertyName == nameof(OutputFormatViewModel.UseAudio))
            {
                OnPropertyChanged(nameof(FileExtension));
            }
        }

        public void AttachPropertyChangeListeners()
        {
            foreach (var item in OutputFormatViewModels)
            {
                item.PropertyChanged += OnOutputFormatViewModelPropertyChanged;
            }
        }

        // When an item is added or removed to the collection, attach or deattach to its property changed event 
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (OutputFormatViewModel item in e.OldItems)
                {
                    item.PropertyChanged -= OnItemPropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (OutputFormatViewModel item in e.NewItems)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OutputFormatViewModel.UseVideo) || e.PropertyName == nameof(OutputFormatViewModel.UseAudio))
            {
                OnPropertyChanged(nameof(FileExtension));
            }
        }

    }
}
