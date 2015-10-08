using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Service.Settings.Models;

namespace Module.Settings.Converters
{
    public class ListOfProcessorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = (IEnumerable<Processor>)value;
            var names = s.Select(n=>n.Name);
            return names;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
