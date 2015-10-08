using System;

namespace Service.HexFile.MemoryMapping
{
    public interface IMemory : IEquatable<IMemory>
    {
        /// <summary>
        /// Memory size
        /// </summary>
        long Size { get; }

        /// <summary>
        /// The number of memory pages
        /// </summary>
        int NumberOfPages { get; }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>The value in cell</returns>
        byte this[long address] { get; set; }

        /// <summary>
        /// Is empty memory
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// The filling factor of memory (the number of non-empty cells)
        /// </summary>
        long FillingFactor { get; }

        /// <summary>
        /// Clear memory
        /// </summary>
        void Clear();

        /// <summary>
        /// Is extended memory
        /// </summary>
        bool IsExtendedMemory { get; }

        /// <summary>
        /// Get memory page
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <returns>Page</returns>
        Page GetPage(int pageNumber);

        /// <summary>
        /// Get memory page by address
        /// </summary>
        /// <param name="pageNumber">Address</param>
        /// <returns>Page</returns>
        Page GetPageByAddress(long address);

        /// <summary>
        /// Set memory page
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="page">Page</param>
        void SetPage(int pageNumber, Page page);
    }
}
