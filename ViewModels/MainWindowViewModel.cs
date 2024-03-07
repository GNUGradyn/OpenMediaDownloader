using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenMediaDownloader.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;
        private bool _loading = false;
        private int _overallProgress = 0;
        private ObservableCollection<DownloadViewModel> _downloads = new ObservableCollection<DownloadViewModel>();

        public MainWindowViewModel()
        {
            _downloads.CollectionChanged += Downloads_CollectionChanged;
            UpdateOverallProgress();
        }

        private void Downloads_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (DownloadViewModel item in e.OldItems)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (DownloadViewModel item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }

            UpdateOverallProgress();
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DownloadViewModel.Progress))
            {
                UpdateOverallProgress();
            }
        }

        private void UpdateOverallProgress()
        {
            if (_downloads.Any())
            {
                OverallProgress = (int)Math.Round(_downloads.Select(x => x.Progress).Average());
                OnPropertyChanged(nameof(OverallProgress));
            }
            else
            {
                OverallProgress = 0; // Or whatever makes sense when there are no downloads.
                OnPropertyChanged(nameof(OverallProgress));
            }
        }

        public ObservableCollection<DownloadViewModel> Downloads => _downloads; // Initalized empty, no setter needed


        public int OverallProgress
        {
            get => _overallProgress;
            set
            {
                _overallProgress = value;
                OnPropertyChanged(nameof(OverallProgress));
            }
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                OnPropertyChanged(nameof(Loading));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
