using System;
using Service.Settings.Enums;

namespace Service.Settings.Extensions
{
    internal static class MemoryExtensions
    {
        internal static int ToInt32(this MemoryType memoryType)
        {
            return (int)memoryType;
        }

        internal static string ToString(this MemoryType memoryType, bool asInt32)
        {
            return (asInt32) ? (memoryType.ToInt32()).ToString() : memoryType.ToString();
        }

        internal static MemoryType ToMemoryType(this int memoryType)
        {
            return (MemoryType)memoryType;
        }

        internal static MemoryType ToMemoryType(this string memoryType, bool asInt32)
        {
            return (asInt32) ? (MemoryType)Convert.ToInt32(memoryType) : (MemoryType)Enum.Parse(typeof(MemoryType), memoryType);
        }     
    }
}
