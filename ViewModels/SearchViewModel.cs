using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMediaDownloader.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private string _searchQuery = "Paste link here";
        private string _searchQueryColor = "#b3b3b3";
        private bool _loading = false;

        public string SearchText
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public string SearchQueryColor
        {
            get => _searchQueryColor;
            set
            {
                if (_searchQueryColor != value)
                {
                    _searchQueryColor = value;
                    OnPropertyChanged(nameof(SearchQueryColor));
                }
            }
        }

        public bool Loading
        {
            get => _loading;
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    OnPropertyChanged(nameof(Loading));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
