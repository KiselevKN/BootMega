using System;
using System.Globalization;
using Service.Settings.Properties;

namespace Service.Settings.Models
{
    public class Processor : IEquatable<Processor>, IFormattable
    {
        #region fields

        public int id;
        public string name;
        public long eepromSize;
        public long flashSize;
        public long bootStartAddress;
        public long bootEndAddress;

        #endregion

        #region properties

        #region public int Id

        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");
                id = value;
            }
            get
            {
                return id;
            }
        }

        #endregion

        #region public string Name

        /// <summary>
        /// Name
        /// </summary>
        public string Name 
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                name = value;
            }
            get
            {
                return name;
            }
        }

        #endregion

        #region public long EepromSize

        /// <summary>
        /// EEPROM size
        /// </summary>
        public long EepromSize
        {
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");
                eepromSize = value;
            }
            get
            {
                return eepromSize;
            }
        }

        #endregion

        #region public long FlashSize
        
        /// <summary>
        /// FLASH size
        /// </summary>
        public long FlashSize
        {
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");
                flashSize = value;
            }
            get
            {
                return flashSize;
            }
        }

        #endregion

        #region public long BootStartAddress

        public long BootStartAddress
        {
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");

                bootStartAddress = value;
            }
            get
            {
                return bootStartAddress;
            }
        }

        #endregion

        #region public long BootEndAddress

        public long BootEndAddress
        {
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");
                bootEndAddress = value;
            }
            get
            {
                return bootEndAddress;
            }
        }

        #endregion

        #endregion

        #region ctors

        public Processor(int id, string name, long eepromSize, long flashSize,
            long bootStartAddress, long bootEndAddress)
        {
            Id = id;
            Name = name;
            EepromSize = eepromSize;
            FlashSize = flashSize;
            BootStartAddress = bootStartAddress;
            BootEndAddress = bootEndAddress;
        }

        private Processor() { }

        #endregion

        #region overrides

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region IEquatable<Processor> Members

        public bool Equals(Processor other)
        {
            return (Id == other.Id &&
                Name == other.Name &&
                EepromSize == other.EepromSize &&
                FlashSize == other.FlashSize &&
                BootStartAddress == other.BootStartAddress 
                && BootEndAddress == other.BootEndAddress);
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
                    return string.Format(Resources.ProcessorString, Id, Name, FlashSize, EepromSize, BootStartAddress, BootEndAddress);
                default:
                    throw new FormatException(string.Format(Resources.FormatNotSupported, format));
            }
        }

        #endregion
    }
}
