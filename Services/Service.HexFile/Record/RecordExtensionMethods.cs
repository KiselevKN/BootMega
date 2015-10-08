using System;
using Service.HexFile.Properties;

namespace Service.HexFile.Record
{
    internal static class RecordExtensionMethods
    {
        /// <summary>
        /// Get record type
        /// </summary>
        /// <param name="record">Record from hex file</param>
        /// <returns>Record type</returns>
        internal static HexRecordType GetRecordType(this string record)
        {
            return (HexRecordType)Convert.ToInt32(record.Substring(7, 2), 16);
        }

        /// <summary>
        /// Get number of data bytes from record
        /// </summary>
        /// <param name="record">Record from hex file</param>
        /// <returns>Number of data bytes</returns>
        internal static int GetNumberOfDataBytes(this string record)
        {
            return Convert.ToInt32(record.Substring(1, 2), 16);
        }

        /// <summary>
        /// Get number of data bytes from record in fact
        /// </summary>
        /// <param name="record">Record from hex file</param>
        /// <returns>Number of data bytes</returns>
        internal static int GetNumberOfDataBytesInFact(this string record)
        {
            return (record.Length - 11) / 2;
        }

        /// <summary>
        /// Get address from record
        /// </summary>
        /// <param name="record">Record from hex file</param>
        /// <returns>Address</returns>
        internal static long GetAddress(this string record)
        {
            switch (record.GetRecordType())
            {
                case HexRecordType.Data:
                    return Convert.ToInt64(record.Substring(3, 4), 16);
                case HexRecordType.ExtendedLinearAddress:
                    return (Convert.ToInt64(record.Substring(9, 4), 16) << 16);
                case HexRecordType.ExtendedSegmentAddress:
                    return (Convert.ToInt64(record.Substring(9, 4), 16) << 4);
                default:
                    throw new ArgumentException(Resources.InvalidRecordWithAddress);
            }
        }

        /// <summary>
        /// Get data bytes from record
        /// </summary>
        /// <param name="record">Record from hex file</param>
        /// <returns>Data bytes</returns>
        internal static byte[] GetDataBytes(this string record)
        {
            if (record.GetRecordType() == HexRecordType.Data)
            {
                byte[] dataBytes = new byte[record.GetNumberOfDataBytes()];
                for (int i = 0; i < record.GetNumberOfDataBytes(); i++)
                    dataBytes[i] = Convert.ToByte(record.Substring(9 + 2 * i, 2), 16);
                return dataBytes;
            }
            throw new ArgumentException(Resources.InvalidRecordWithData);
        }

        /// <summary>
        /// Get checkSum from record
        /// </summary>
        /// <param name="record">Record from hex file</param>
        /// <returns>CheckSum</returns>
        internal static int GetCheckSum(this string record)
        {
            return Convert.ToInt32(record.Substring(record.Length - 2, 2), 16);
        }

        /// <summary>
        /// Get checkSum from record in fact
        /// </summary>
        /// <param name="record">Record from hex file</param>
        /// <returns>CheckSum</returns>
        internal static int GetCheckSumInFact(this string record)
        {
            int checkSum = 0;
            for (int i = 0; i < (record.Length - 1) / 2 - 1; i++)
                checkSum += Convert.ToInt32(record.Substring(1 + 2 * i, 2), 16);
            checkSum &= 0xFF;
            checkSum = 0x100 - checkSum;
            checkSum &= 0xFF;
            return checkSum;
        }
    }
}
