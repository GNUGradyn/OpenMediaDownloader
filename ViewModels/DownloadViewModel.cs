﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using OpenMediaDownloader.Models;

namespace OpenMediaDownloader.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private readonly string _title;
        private readonly BitmapImage _thumbnail;
        private int _progress;
        private readonly string _path;
        private bool _processing = false;
        private bool _done = false;
        
        public DownloadViewModel(string title, BitmapImage thumbnail, string path)
        {
            _title = title;
            _thumbnail = thumbnail;
            _path = path;
        }

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public bool Done
        {
            get => _done;
            set
            {
                _done = value;
                OnPropertyChanged(nameof(Done));
            }
        }

        public bool Processing
        {
            get => _processing;
            set
            {
                _processing = value;
                OnPropertyChanged(nameof(Processing));
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