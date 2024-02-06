using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OpenMediaDownloader.ViewModels
{
    internal class DownloadWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private BitmapImage _thumbnail;
        private int _originalWidth;
        private int _originalheight;

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

        public int OriginalWidth
        {
            get
            {
                return _originalWidth;
            }
            set
            {
                _originalWidth = value;
                OnPropertyChanged(nameof(OriginalWidth));
            }
        }

        public int OriginalHeight
        {
            get
            {
                return _originalheight;
            }
            set
            {
                _originalheight = value;
                OnPropertyChanged(nameof(OriginalHeight));
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


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
