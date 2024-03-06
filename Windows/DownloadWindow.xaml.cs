using System.IO;
using OpenMediaDownloader.Models;
using OpenMediaDownloader.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenMediaDownloader.Windows
{
    /// <summary>
    /// Interaction logic for DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {

        public DownloadWindow()
        {
            InitializeComponent();
        }

        private void BrowseForDirectory(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                ((DownloadWindowViewModel)DataContext).Path = dialog.SelectedPath;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DownloadWindowViewModel viewModel = DataContext as DownloadWindowViewModel;
            CheckBox checkbox = sender as CheckBox;

            // Find the DataGridRow that contains the checkbox
            DataGridRow row = FindParent<DataGridRow>(checkbox);
            if (row == null) return;

            // Get the OutputFormatViewModel for the row
            OutputFormatViewModel currentViewModel = row.DataContext as OutputFormatViewModel;
            if (currentViewModel == null) return;

            string columnHeader = ((DataGridCell)checkbox.Parent).Column.Header.ToString();
            if (columnHeader == "Use Video")
            {
                viewModel.OutputFormatViewModels.ToList().ForEach(x =>
                {
                    x.UseVideo = x == currentViewModel;
                });
            }
            else if (columnHeader == "Use Audio")
            {
                viewModel.OutputFormatViewModels.ToList().ForEach(x =>
                {
                    x.UseAudio = x == currentViewModel;
                });
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DownloadWindowViewModel viewModel = DataContext as DownloadWindowViewModel;
            CheckBox checkbox = sender as CheckBox;

            // Find the DataGridRow that contains the checkbox
            DataGridRow row = FindParent<DataGridRow>(checkbox);
            if (row == null) return;

            // Get the OutputFormatViewModel for the row
            OutputFormatViewModel currentViewModel = row.DataContext as OutputFormatViewModel;
            if (currentViewModel == null) return;

            string columnHeader = ((DataGridCell)checkbox.Parent).Column.Header.ToString();

            if (columnHeader == "Use Video")
            {
                viewModel.OutputFormatViewModels.ToList().ForEach((x) =>
                {
                    x.UseVideo = false;
                });
            }
            else if (columnHeader == "Use Audio")
            {
                viewModel.OutputFormatViewModels.ToList().ForEach((x) =>
                {
                    x.UseAudio = false;
                });
            };
        }

        // Helper method to find a parent of a given control/item
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            DownloadWindowViewModel viewModel = DataContext as DownloadWindowViewModel;
            var newDownload = new DownloadViewModel(
                viewModel.Title,
                viewModel.Thumbnail,
                Path.Combine(viewModel.Path, viewModel.Filename + viewModel.FileExtension)
                );
            ((MainWindowViewModel)Application.Current.MainWindow.DataContext).Downloads.Add(newDownload);
            var dlsvc = new YoutubeDlService();
            dlsvc.DownloadProgressChanged += (int progress) => newDownload.Progress = progress;
            dlsvc.DownloadAndTrack(
                viewModel.Url, 
                Path.Combine(viewModel.Path, viewModel.Filename), 
                viewModel.OutputFormatViewModels.SingleOrDefault(x => x.UseVideo).FormatId ?? "none", 
                viewModel.OutputFormatViewModels.SingleOrDefault(x => x.UseAudio).FormatId ?? "none");
            Close();
        }
    }
}
