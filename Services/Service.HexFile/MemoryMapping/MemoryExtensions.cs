namespace Service.HexFile.MemoryMapping
{
    internal static class MemoryExtensions
    {
        /// <summary>
        /// Size of the jagged array
        /// </summary>
        internal static long Size<T>(this T[][] array)
        {
            long size = 0;
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i].Length; j++)
                    ++size;

            return size;
        }

        /// <summary>
        /// Create array of segments by memory size
        /// </summary>
        internal static T[][] Create<T>(long size)
        {
            var numberOfSegments = (int)(size >> 16);
            var remainderOfTheDivision = (int)(size & 0xFFFF);

            if (remainderOfTheDivision != 0)
                numberOfSegments++;

            var array = new T[numberOfSegments][];

            for (int i = 0; i < numberOfSegments; i++)
            {
                array[i] = ((i == numberOfSegments - 1) && (remainderOfTheDivision != 0)) ?
                    new T[remainderOfTheDivision] :
                    new T[Memory.SegmentSize];
            }

            return array;
        }
    }
}
