using System;
using System.Windows.Data;
using System.Globalization;

namespace Module.HexFile.Converters
{
    public class MaxScrollConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long maxAddress = (long)value;
            double max = (double)(maxAddress / 0x10);
            return max;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
