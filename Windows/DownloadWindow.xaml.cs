using OpenMediaDownloader.ViewModels;
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
using System.Windows.Shapes;

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
            ContentRendered += DownloadWindow_ContentRendered;
        }

        // https://stackoverflow.com/questions/72120715/wpf-window-need-to-use-sizetocontent-up-to-a-specific-size  
        private void DownloadWindow_ContentRendered(object sender, EventArgs e)
        {
            // Clear the MaxWidth/MaxHeight so that you can manually resize larger
            MaxWidth = double.PositiveInfinity;
            MaxHeight = double.PositiveInfinity;
            // Clear the SizeToContent so that it doesn't automatically resize to large datasets
            SizeToContent = SizeToContent.Manual;
        }
    }
}
