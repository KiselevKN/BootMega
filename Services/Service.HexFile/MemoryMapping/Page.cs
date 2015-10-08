namespace Service.HexFile.MemoryMapping
{
    using System;

    /// <summary>
    /// Memory page
    /// </summary>
    public class Page
    {
        #region consts

        /// <summary>
        /// Number of page lines
        /// </summary>
        public const int NumberOfLines = 16;

        /// <summary>
        /// Page size
        /// </summary>
        public const int Size = 256;

        /// <summary>
        /// Empty cell value
        /// </summary>
        public const byte EmptyCell = 0xFF;

        #endregion

        #region fiels

        private byte[] cells;

        #endregion

        #region ctors

        public Page()
        {
            cells = new byte[Size];
        }

        #endregion

        #region indexer

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Page.Size)
                    throw new IndexOutOfRangeException("index");

                return cells[index];
            }
            set
            {
                if (index < 0 || index >= Page.Size)
                    throw new IndexOutOfRangeException("index");

                cells[index] = value;
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// Is empty page
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < cells.Length; i++)
                    if (cells[i] != EmptyCell)
                        return false;
                return true;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Get page line
        /// </summary>
        /// <param name="lineNumber">Line number</param>
        /// <returns>Line</returns>
        public PageLine GetLine(int lineNumber)
        {
            if (lineNumber < 0 || lineNumber >= NumberOfLines)
                throw new IndexOutOfRangeException("lineNumber");

            PageLine line = new PageLine();

            for (int i = 0; i < PageLine.Size; i++)
                line[i] = cells[lineNumber * PageLine.Size + i];

            return line;
        }

        /// <summary>
        /// Set page line
        /// </summary>
        /// <param name="lineNumber">Line number</param>
        /// <param name="line">Line</param>
        public void SetLine(int lineNumber, PageLine line)
        {
            if (lineNumber < 0 || lineNumber >= NumberOfLines)
                throw new IndexOutOfRangeException("lineNumber");

            for (int i = 0; i < PageLine.Size; i++)
                cells[lineNumber * PageLine.Size + i] = line[i];
        }

        #endregion

        #region operators

        public static implicit operator byte[](Page page)
        {
            return page.cells;
        }

        #endregion
    }
}
