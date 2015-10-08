using System;
using System.Collections.Generic;
using System.Text;

namespace Service.HexFile.Record
{
    internal static class EnumerableExtensionMethods
    {
        /// <summary>
        /// Data bytes to hex string
        /// </summary>
        /// <param name="data">Data bytes</param>
        /// <returns>Hex string</returns>
        internal static String ToHexString(this IEnumerable<Byte> bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
                sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }

        /// <summary>
        /// Data bytes checksum
        /// </summary>
        /// <param name="bytes">Data bytes</param>
        /// <returns>Checksum</returns>
        internal static Byte Checksum(this IEnumerable<Byte> bytes)
        {
            Int32 checkSum = 0;
            foreach (var b in bytes)
                checkSum += b;
            checkSum &= 0xFF;
            checkSum = 0x100 - checkSum;
            checkSum &= 0xFF;
            return (Byte)checkSum;
        }
    }
}
