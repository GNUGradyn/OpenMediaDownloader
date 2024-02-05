using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMediaDownloader.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchQuery = "Paste link here";
        private string _searchQueryColor = "#b3b3b3";

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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
