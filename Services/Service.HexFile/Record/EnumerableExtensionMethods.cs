﻿using System;
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
        internal static string ToHexString(this IEnumerable<byte> bytes)
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
        internal static byte Checksum(this IEnumerable<byte> bytes)
        {
            int checkSum = 0;
            foreach (var b in bytes)
                checkSum += b;
            checkSum &= 0xFF;
            checkSum = 0x100 - checkSum;
            checkSum &= 0xFF;
            return (byte)checkSum;
        }
    }
}
