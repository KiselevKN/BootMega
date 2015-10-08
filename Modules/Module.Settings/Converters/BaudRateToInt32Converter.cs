using System;
using System.Windows.Data;
using Service.Settings.Enums;

namespace Module.Settings.Converters
{
    public class BaudRateToInt32Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)Enum.Parse(typeof(BaudRate), ((BaudRate)value).ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
