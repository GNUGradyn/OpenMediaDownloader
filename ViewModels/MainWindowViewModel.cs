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
        private ObservableCollection<DownloadViewModel> _downloads = new ObservableCollection<DownloadViewModel>();

        public MainWindowViewModel()
        {
            _downloads.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Downloads));
        }
        
        public ObservableCollection<DownloadViewModel> Downloads => _downloads; // Initalized empty, no setter needed

        public string SearchText
        {
            get 
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
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
