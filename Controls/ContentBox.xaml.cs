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

namespace OpenMediaDownloader.Controls
{
    /// <summary>
    /// Interaction logic for ContentBox.xaml
    /// </summary>
    public partial class ContentBox : UserControl
    {

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ContentBox), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ContentBox()
        {
            InitializeComponent();
        }
    }
}
