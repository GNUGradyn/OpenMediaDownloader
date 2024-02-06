﻿using OpenMediaDownloader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenMediaDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IYoutubeDlService _youtubeDlService;

        public MainWindow()
        {
            InitializeComponent();

            _youtubeDlService = new YoutubeDlService();
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            //var viewModel = (SearchViewModel)DataContext;
            //if (string.IsNullOrWhiteSpace(viewModel.SearchText)) return;
            //Console.WriteLine($"Attempting to download {viewModel.SearchText}");
        }

        private void Search_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
