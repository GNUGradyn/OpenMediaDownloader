using OpenMediaDownloader.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OpenMediaDownloader
{
    public class FormatOptionArrayToTableObjectArray : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var formatOptionArray = (value as FormatOption[]);
            return formatOptionArray.ToList().Select(x => new
            {
                Height = x.Height.ToString() ?? "N/A",
                Width = x.Width.ToString() ?? "N/A",
                Container = string.IsNullOrEmpty(x.Container) ? "Unknown" : x.Container,
                AudioCodec = GetAudioCodecName(x.AudioCodec),
                VideoCodec = GetVideoCodecName(x.VideoCodec),
                FPS = x.FPS.ToString() ?? "Unknown"
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetAudioCodecName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Unknown";
            if (name == "none") return "Video Only";
            return name;
        }

        private string GetVideoCodecName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Unknown";
            if (name == "none") return "Audio Only";
            return name;
        }
    }
}
