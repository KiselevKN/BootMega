using System;
using System.Collections.Generic;
using System.IO;
using Service.HexFile.MemoryMapping;
using Service.HexFile.Record;
using Service.HexFile.Record.Parsing;
using Service.HexFile.Record.Validation;

namespace Service.HexFile
{
    internal static class HexFileManagerExtensions
    {
        internal static void Validation(this FileStream fs, IHexRecordValidator validator)
        {
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
                validator.Validate(sr.ReadLine());
        }

        internal static List<HexRecord> Parsing(this FileStream fs, IHexRecordParser parser)
        {
            List<HexRecord> list = new List<HexRecord>();

            using (StreamReader sr = new StreamReader(fs))
            {
                String record;
                while (!sr.EndOfStream)
                {
                    record = sr.ReadLine();
                    if ((record.GetRecordType() == HexRecordType.ExtendedLinearAddress) ||
                        (record.GetRecordType() == HexRecordType.ExtendedSegmentAddress) ||
                        (record.GetRecordType() == HexRecordType.Data))
                        list.Add(parser.Parse(record));
                }
            }
            return list;
        }

        internal static void FillMemory(this List<HexRecord> records, IMemory memory)
        {
            long segmentAddress = 0;

            foreach (var record in records)
            {
                var type = record.RecordType;
                var address = record.Address;
                if (type == HexRecordType.ExtendedLinearAddress || type == HexRecordType.ExtendedSegmentAddress)
                    segmentAddress = address;
                else if (type == HexRecordType.Data)
                {
                    address += segmentAddress;
                    Byte[] data = ((DataHexRecord)record).Data;
                    int i = 0;
                    foreach (byte b in data)
                    {
                        memory[address + i] = b;
                        i++;
                    }
                }
            }
        }

        internal static byte[] Copy(this byte[] array)
        {
            byte[] newArray = new byte[array.Length];

            for (int i = 0; i < array.Length; i++)
                newArray[i] = array[i];

            return newArray;
        }

        internal static bool IsEmpty(this byte[] array)
        {
            for (int k = 0; k < array.Length; k++)
                if (array[k] != Memory.EmptyCell)
                    return false;
            return true;
        }


        internal static List<HexRecord> ToHexRecords(this IMemory memory)
        {
            List<HexRecord> hexRecordList = new List<HexRecord>();
            byte[] line = new byte[16];
            bool firstSegmentAddress = false;
            AddressHexRecord addressHexRecord = null;
            for (long i = 0; i < memory.Size; i++)
            {
                if ((i & 0xFFFF) == 0)
                {
                    HexRecordType type = (memory.IsExtendedMemory == true) ? 
                        HexRecordType.ExtendedLinearAddress : 
                        HexRecordType.ExtendedSegmentAddress;
                    addressHexRecord = new AddressHexRecord(type, i);
                    firstSegmentAddress = true;
                }
                line[(int)(i & 0x0F)] = memory[i];
                if ((int)(i & 0x0F) == 0xF)
                {
                    if (!line.IsEmpty())
                    {
                        if (firstSegmentAddress)
                        {
                            firstSegmentAddress = false;
                            hexRecordList.Add(addressHexRecord);
                        }
                        hexRecordList.Add(new DataHexRecord(HexRecordType.Data, i - 0xF, line.Copy()));
                    }
                }
            }
            return hexRecordList;
        }

        internal static void Parsing(this List<HexRecord> records, FileStream fs, IHexRecordParser parser)
        {
            using (StreamWriter sr = new StreamWriter(fs))
            {
                foreach(var record in records)
                    sr.WriteLine(parser.UnParse(record));
                sr.WriteLine(HexRecord.EOF);
            }
        }

    }
}
