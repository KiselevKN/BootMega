using System;
using System.Globalization;
using Service.Settings.Properties;

namespace Service.Settings.Models
{
    public class SettingsInfo : IEquatable<SettingsInfo>, IFormattable
    {
        #region fields

        private int id;
        private string name;
        private Processor processor;
        private SerialPortSettings serialPortSettings;

        #endregion

        #region properties

        public int Id 
        { 
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");

                id = value;
            }
            get { return id; }
        }

        public string Name
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                name = value;
            }
            get { return name; }
        }

        public Processor Processor
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                processor = value;
            }
            get { return processor; }
        }

        public SerialPortSettings SerialPortSettings
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                serialPortSettings = value;
            }
            get { return serialPortSettings; }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return Name;
        }

        #endregion
    
        #region ctors

        public SettingsInfo(int id, string name, Processor processor,
            SerialPortSettings serialPortSettings)
        {
            Id = id;
            Name = name;
            Processor = processor;
            SerialPortSettings = serialPortSettings;
        }

        private SettingsInfo() { }

        #endregion

        #region IEquatable<SettingsInfo> Members

        public bool Equals(SettingsInfo other)
        {
            return (Id == other.Id && Name == other.Name &&
                Processor.Equals(other.Processor) &&
                SerialPortSettings.Equals(other.SerialPortSettings)) ? true : false;
        }

        #endregion  
    
        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";
            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return ToString();
                case "LOG":
                    return string.Format(Resources.SettingsInfoString,
                        Id, Name, Processor.ToString("LOG", null), SerialPortSettings.ToString("LOG", null));
                default:
                    throw new FormatException(string.Format(Resources.FormatNotSupported, format));
            }
        }

        #endregion
    }
}
