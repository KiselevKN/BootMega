using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Module.HexFile.ViewModels;

namespace Module.HexFile.Converters
{
    public class ListOfFilesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = (IEnumerable<HexFileViewModel>)value;
            var names = s.Select(n=>n.FileName);
            return names;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
