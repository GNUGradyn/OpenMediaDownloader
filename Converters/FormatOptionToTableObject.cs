using OpenMediaDownloader.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OpenMediaDownloader.Converters
{
    public class FormatOptionArrayToTableObjectArray : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var formatOptionArray = (value as FormatOption[]);
            //return formatOptionArray.Select<FormatOption>(new object
            //{

            //}).ToArray();
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
