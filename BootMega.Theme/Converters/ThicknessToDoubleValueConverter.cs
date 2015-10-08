using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BootMega.Theme.Converters
{
    public class ThicknessToDoubleValueConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness t = (Thickness)value;
            string p = parameter as string;
            if (t != null)
            {
                switch(p.ToLower())
                {
                    case "top": return t.Top;
                    case "left": return t.Left;
                    case "right": return t.Right;
                    case "bottom": return t.Bottom;
                }
            }
            return 0.0;
        }
 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
