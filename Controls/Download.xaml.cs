using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OpenMediaDownloader.Controls
{
    public partial class Download : UserControl
    {
        public Download()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(float), typeof(Download), new PropertyMetadata(0f));
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Download), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(Download), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ProcessingProperty =
            DependencyProperty.Register("Processing", typeof(bool), typeof(Download), new PropertyMetadata(false));
        public static readonly DependencyProperty DoneProperty =
            DependencyProperty.Register("Done", typeof(bool), typeof(Download), new PropertyMetadata(false));
        public static readonly DependencyProperty ThumbnailProperty =
            DependencyProperty.Register("Thumbnail", typeof(BitmapImage), typeof(Download), new PropertyMetadata(null));


        public string Progress
        {
            get => (string)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public bool Processing
        {
            get => (bool)GetValue(ProcessingProperty);
            set => SetValue(ProcessingProperty, value);
        }

        public bool Done
        {
            get => (bool)GetValue(DoneProperty);
            set => SetValue(DoneProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Path
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public BitmapImage Thumbnail
        {
            get => (BitmapImage)GetValue(ThumbnailProperty);
            set => SetValue(ThumbnailProperty, value);
        }
    }
}