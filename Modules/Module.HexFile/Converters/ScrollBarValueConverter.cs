using System;
using System.Windows.Data;
using System.Globalization;

namespace Module.HexFile.Converters
{
    public class ScrollBarValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long v = (long)value;
            return v / 0x10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = (double)value;
            return v * 0x10;
        }
    }
}
