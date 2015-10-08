using System;
using System.Windows.Data;
using System.Globalization;

namespace Module.HexFile.Converters
{
    public class ValueScrollConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long v = (long)value;
            double max = (double)(v/0x10);
            return max;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = (double)value;
            return (object)(v * 0x10);
        }
    }
}
