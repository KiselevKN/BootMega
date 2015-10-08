namespace Service.HexFile.MemoryMapping
{
    using System;
    using System.Globalization;
    using System.Text;
    using Service.HexFile.Properties;

    /// <summary>
    /// Page line
    /// </summary>
    public class PageLine : IFormattable
    {
        #region consts

        /// <summary>
        /// Line size
        /// </summary>
        public const int Size = 16;

        #endregion

        #region fields

        private byte[] cells;

        #endregion

        #region ctors

        public PageLine()
        {
            cells = new byte[16];
        }

        #endregion

        #region indexer

        public byte this[int index]
        {
            get 
            {
                if (index < 0 || index >= PageLine.Size)
                    throw new IndexOutOfRangeException("index");

                return cells[index]; 
            }
            set 
            {
                if (index < 0 || index >= PageLine.Size)
                    throw new IndexOutOfRangeException("index");

                cells[index] = value; 
            }
        }

        #endregion

        #region operators

        public static implicit operator byte[](PageLine line)
        {
            return line.cells;
        }

        #endregion

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (String.IsNullOrEmpty(format))
                format = "G";

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;
            
            StringBuilder sb = new StringBuilder();

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return ToString();
                case "H":                   
                    for (int i = 0; i < Size; i++)
                        sb.AppendFormat("{0:X2} ", cells[i]);
                    return sb.ToString().Trim();
                case "C":
                    for (int i = 0; i < Size; i++)
                        sb.AppendFormat("{0} ", (char)cells[i]);
                    return sb.ToString().Trim();
                default:
                    throw new FormatException(String.Format(Resources.FormatNotSupported, format));
            }
        }

        #endregion
    }
}
