using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.HexFile.Record;
using Service.HexFile.Record.Parsing;

namespace BootMega.UnitTests
{
    [TestClass]
    public class RecordParsingTests
    {
        [TestMethod]
        [TestCategory("RecordParsing")]
        public void HexRecordParse()
        {
            Action<HexRecord, string> func = (expectedHexRecord, record) =>
            {
                IHexRecordParser parser = new HexRecordParser();
                HexRecord r = parser.Parse(record);
                Assert.AreEqual<HexRecordType>(expectedHexRecord.RecordType, r.RecordType);
                Assert.AreEqual<long>(expectedHexRecord.Address, r.Address);
                if (expectedHexRecord is DataHexRecord)
                {
                    Assert.IsTrue(r is DataHexRecord);
                    Assert.IsTrue(Enumerable.SequenceEqual(((DataHexRecord)expectedHexRecord).Data, ((DataHexRecord)r).Data));
                }
            };

            func(new AddressHexRecord(HexRecordType.ExtendedSegmentAddress, 0x10000), ":020000021000EC");
            Assert.AreEqual(":00000001FF", HexRecord.EOF);
            func(new AddressHexRecord(HexRecordType.ExtendedSegmentAddress, 0), ":020000020000FC");
            func(new DataHexRecord(HexRecordType.Data, 0, new byte[] { 0xFA, 0x00, 0x00, 0x02 }), ":04000000FA00000200");
            func(new DataHexRecord(HexRecordType.Data, 0xC220, new byte[] { 0xF0, 0x4E, 0xF0, 0x5F, 0xF0, 0x6C, 0xF0, 0x7D, 
                0xCA, 0x00, 0x50, 0xC2, 0xF0, 0x86, 0xF0, 0x97 }), 
                ":10C22000F04EF05FF06CF07DCA0050C2F086F097DF");
        }

        [TestMethod]
        [TestCategory("RecordParsing")]
        public void HexRecordUnParse()
        {
            Action<string, HexRecord> func = (expectedRecord, hexRecord) =>
            {
                IHexRecordParser parser = new HexRecordParser();
                string r = parser.UnParse(hexRecord);
                Assert.AreEqual(expectedRecord, r);
            };

            func(":020000021000EC", new AddressHexRecord(HexRecordType.ExtendedSegmentAddress, 0x10000));
            func(":020000020000FC", new AddressHexRecord(HexRecordType.ExtendedSegmentAddress, 0));
            func(":04000000FA00000200", new DataHexRecord(HexRecordType.Data, 0, new byte[] { 0xFA, 0x00, 0x00, 0x02 }));
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DF", 
                new DataHexRecord(HexRecordType.Data, 0xC220, 
                    new byte[] { 0xF0, 0x4E, 0xF0, 0x5F, 0xF0, 0x6C, 0xF0, 0x7D, 
                        0xCA, 0x00, 0x50, 0xC2, 0xF0, 0x86, 0xF0, 0x97 }));
        }
    }
}
