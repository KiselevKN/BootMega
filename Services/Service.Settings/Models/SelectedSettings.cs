using System;
using System.Globalization;
using Service.Settings.Enums;
using Service.Settings.Extensions;
using Service.Settings.Properties;

namespace Service.Settings.Models
{
    public class SelectedSettings: IFormattable
    {
        #region fields

        public SettingsInfo settingsInfo;

        #endregion

        #region properties

        public MemoryType MemoryType { get; set; }

        public SettingsInfo SettingsInfo
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                settingsInfo = value;
            }
            get
            {
                return settingsInfo;
            }
        }

        #endregion

        #region ctors

        public SelectedSettings(MemoryType memoryType, SettingsInfo settingsInfo)
        {
            MemoryType = memoryType;
            SettingsInfo = settingsInfo;
        }

        private SelectedSettings() { }

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
                    return string.Format(Resources.SelectedSettingsString,
                        SettingsInfo.ToString("LOG", null), MemoryType.ToString(false));
                default:
                    throw new FormatException(string.Format(Resources.FormatNotSupported, format));
            }
        }

        #endregion
    }
}
