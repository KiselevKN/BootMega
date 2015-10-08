using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace Module.Settings.Converters
{
    public class HeadersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = (IEnumerable<byte>)value;
            List<string> sHeaders = new List<string>();

            foreach (var b in s)
                sHeaders.Add(String.Format("0x{0:X2}", b));

            return sHeaders;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
