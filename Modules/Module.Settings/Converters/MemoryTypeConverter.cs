using System;
using System.Windows.Data;
using Service.Settings.Enums;

namespace Module.Settings.Converters
{
    public class MemoryTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((MemoryType)value == MemoryType.FLASH) ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return ((bool)value == true) ? MemoryType.FLASH : MemoryType.EEPROM;
        }
    }
}
