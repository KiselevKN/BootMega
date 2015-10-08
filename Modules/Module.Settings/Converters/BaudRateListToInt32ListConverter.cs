using System;
using System.Collections.Generic;
using System.Windows.Data;
using Service.Settings.Enums;

namespace Module.Settings.Converters
{
    public class BaudRateListToInt32ListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = (IEnumerable<BaudRate>)value;
            List<int> iBaudRates = new List<int>();

            foreach (var b in s)
                iBaudRates.Add((int)Enum.Parse(typeof(BaudRate), b.ToString()));

            return iBaudRates;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
