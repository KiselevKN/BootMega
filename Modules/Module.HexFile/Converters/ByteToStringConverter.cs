using System;
using System.Windows.Data;
using System.Globalization;

namespace Module.HexFile.Converters
{
    public class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte v = (byte)value;
            var ch = System.Convert.ToChar(v);
            var condition = (char.IsDigit(ch) || char.IsLetter(ch) || char.IsPunctuation(ch));
            return ((v < 0x80) && condition) ? (object)ch.ToString() : (object)" ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
