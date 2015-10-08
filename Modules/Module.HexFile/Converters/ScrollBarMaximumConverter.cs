using System;
using System.Windows.Data;
using System.Globalization;

namespace Module.HexFile.Converters
{
    public class ScrollBarMaximumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long max = (long)value;
            return max / 0x10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
