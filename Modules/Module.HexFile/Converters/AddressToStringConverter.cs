using System;
using System.Globalization;
using System.Windows.Data;

namespace Module.HexFile.Converters
{
    public class AddressToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = (long)values[0];
            var maximum = (long)values[1];
            var lenght = maximum.ToString("X").Length;
            return value.ToString("X" + lenght);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        { 
            throw new NotImplementedException();       
        }
    }
}
