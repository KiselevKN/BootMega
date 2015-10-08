using System;
using System.Globalization;
using System.Windows.Data;
using Module.HexFile.ViewModels;

namespace Module.HexFile.Converters
{
    public class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HexFileViewModel)
                return value;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HexFileViewModel)
                return value;

            return Binding.DoNothing;
        }
    }
}
