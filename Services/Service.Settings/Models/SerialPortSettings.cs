using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using Service.Settings.Enums;
using Service.Settings.Extensions;
using Service.Settings.Properties;

namespace Service.Settings.Models
{
    public class SerialPortSettings : IEquatable<SerialPortSettings>, IFormattable
    {
        #region consts

        public const byte MinAllowableHeaderValue = 0x80;
        public const byte MaxAllowableHeaderValue = 0xFF;

        #endregion

        #region fields

        private byte headerTX;
        private byte headerRX;

        #endregion

        #region properties

        public BaudRate BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }

        public byte HeaderTX 
        {
            get {  return headerTX; }
            set {
                if (value < MinAllowableHeaderValue)
                    throw new ArgumentException("value");

                headerTX = value;
            }
        }

        public byte HeaderRX
        {
            get { return headerRX; }
            set {
                if (value < MinAllowableHeaderValue)
                    throw new ArgumentException("value");

                headerRX = value;
            }
        }

        public static List<byte> AllowableHeadersList
        {
            get
            {
                var allowableHeadersList = new List<byte>();

                for (int i = MinAllowableHeaderValue; i <= MaxAllowableHeaderValue; i++)
                    allowableHeadersList.Add((byte)i);

                return allowableHeadersList;
            }
        }

        #endregion

        #region ctors

        public SerialPortSettings(BaudRate baudRate, Parity parity, StopBits stopBits, byte headerTX, byte headerRX)
        {
            BaudRate = baudRate;
            Parity = parity;
            StopBits = stopBits;
            HeaderTX = headerTX;
            HeaderRX = headerRX;
        }

        private SerialPortSettings() { }

        #endregion

        #region IEquatable<SerialPortSettings> Members

        public bool Equals(SerialPortSettings other)
        {
            return (BaudRate == other.BaudRate &&
                Parity == other.Parity &&
                StopBits == other.StopBits &&
                HeaderTX == other.HeaderTX &&
                HeaderRX == other.HeaderRX);
        }

        #endregion

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
                format = "G";

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return ToString();
                case "LOG":
                    return String.Format(Resources.SerialPortSettingsString,
                        BaudRate.ToString(true), Parity.ToString(), StopBits.ToString(), headerTX, HeaderRX);
                default:
                    throw new FormatException(String.Format(Resources.FormatNotSupported, format));
            }
        }

        #endregion
    }
}
