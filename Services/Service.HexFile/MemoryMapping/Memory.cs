namespace Service.HexFile.MemoryMapping
{
    using System;
    using System.Collections.Generic;
    using Service.HexFile.Properties;

    public class Memory : IMemory
    {
        #region consts

        /// <summary>
        /// Maximum size of extended memory
        /// </summary>
        public const long MaxExtendedMemorySize = 0x100000000;
        
        /// <summary>
        /// Maximum memory size
        /// </summary>
        public const long MaxMemorySize = 0x100000;

        /// <summary>
        /// Memory segment size
        /// </summary>
        public const long SegmentSize = 0x10000;
        
        /// <summary>
        /// Page size
        /// </summary>
        public const long PageSize = 0x100;
        
        /// <summary>
        /// Empty cell value
        /// </summary>
        public const byte EmptyCell = 0xFF;

        #endregion

        #region fields

        private byte[][] segments;
        private long size;

        #endregion

        #region ctors
        
        private Memory() { }

        public Memory(long size)
        {
            if (size <= 0 || size > MaxExtendedMemorySize)
                throw new ArgumentException(Resources.InvalidMemorySize);

            if (size % PageSize != 0)
                throw new ArgumentException(Resources.InvalidMemorySize);

            segments = MemoryExtensions.Create<byte>(size);
            this.size = segments.Size();

            Clear();
        }

        #endregion

        #region IEquatable<IMemory> Members

        public bool Equals(IMemory other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (other == this)
                return true;

            if (Size != other.Size)
                throw new ArgumentException(Resources.IncorrectComparisionOfMemoryAreas);

            for (long i = 0; i < Size; i++)
                if (this[i] != other[i])
                    return false;

            return true;
        }

        #endregion

        #region methods

        public static bool Equals(IMemory memoryLeft, IMemory memoryRight, out List<long> differences)
        {
            bool result = true;

            if (memoryLeft == null)
                throw new ArgumentNullException("memoryLeft");

            if (memoryLeft.Size != memoryRight.Size)
                throw new ArgumentException(Resources.IncorrectComparisionOfMemoryAreas);

            differences = new List<long>();

            for (long i = 0; i < memoryRight.Size; i++)
                if (memoryLeft[i] != memoryRight[i])
                {
                    result = false;
                    differences.Add(i);
                }

            return result;
        }

        #endregion

        #region IMemory Members

        public long Size
        {
            get { return size; }
        }

        public int NumberOfPages
        {
            get { return (int)(Size / Page.Size); }
        }

        public byte this[long address]
        {
            get
            {
                if (address < 0 || address >= Size)
                    throw new IndexOutOfRangeException(String.Format(Resources.AddressOutOfRange, address, 0, Size - 1));

                int segmentNumber = (int)(address >> 16);
                int remainderOfTheDivision = (int)(address & 0xFFFF);
                return segments[segmentNumber][remainderOfTheDivision];
            }
            set
            {
                if (address < 0 || address >= Size)
                    throw new IndexOutOfRangeException(String.Format(Resources.AddressOutOfRange, address, 0, Size - 1));

                int segmentNumber = (int)(address >> 16);
                int remainderOfTheDivision = (int)(address & 0xFFFF);
                segments[segmentNumber][remainderOfTheDivision] = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < segments.Length; i++)
                    for (int j = 0; j < segments[i].Length; j++)
                        if (segments[i][j] != EmptyCell)
                            return false;

                return true;
            }
        }

        public long FillingFactor
        {
            get
            {
                long factor = 0;
                for (int i = 0; i < segments.Length; i++)
                    for (int j = 0; j < segments[i].Length; j++)
                        if (segments[i][j] != EmptyCell)
                            factor++;

                return factor;
            }
        }

        public void Clear()
        {
            for (long i = 0; i < Size; i++)
                this[i] = EmptyCell;
        }

        public bool IsExtendedMemory
        {
            get { return (size > MaxMemorySize); }
        }

        public Page GetPage(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber >= NumberOfPages)
                throw new IndexOutOfRangeException("pageNumber");

            Page page = new Page();

            for (int i = 0; i < Page.Size; i++)
                page[i] = this[pageNumber * Page.Size + i];

            return page;
        }

        public Page GetPageByAddress(long address)
        {
            if (address < 0 || address > Size - Page.Size)
                throw new IndexOutOfRangeException(String.Format(Resources.AddressOutOfRange, address, 0, Size - Page.Size));

            Page page = new Page();
            for (int i = 0; i < Page.Size; i++)
                page[i] = this[address + i];

            return page;
        }

        public void SetPage(int pageNumber, Page page)
        {
            if (pageNumber < 0 || pageNumber >= NumberOfPages)
                throw new IndexOutOfRangeException("pageNumber");

            if (page == null)
                throw new ArgumentNullException("page");

            for (int i = 0; i < Page.Size; i++)
                this[pageNumber * Page.Size + i] = page[i];
        }

        #endregion
    }
}
