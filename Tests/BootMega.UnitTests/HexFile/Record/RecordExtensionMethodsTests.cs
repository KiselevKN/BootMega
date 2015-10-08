using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.HexFile.Record;

namespace BootMega.UnitTests
{
    [TestClass]
    public class RecordExtensionMethodsTests
    {
        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        public void GetRecordType()
        {
            Action<string, HexRecordType> func = (record, expectedValue) =>
            {
                var type = record.GetRecordType();
                Assert.AreEqual<HexRecordType>(expectedValue, type);
            };

            func(":020000020000FC", HexRecordType.ExtendedSegmentAddress);
            func(":100000000C943E010C9430350C9431350C943235FF", HexRecordType.Data);
            func(":00000001FF", HexRecordType.EOF);
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        public void GetNumberOfDataBytes()
        {
            Action<string, int> func = (record, expectedValue) =>
            {
                var numberOfDataBytes = record.GetNumberOfDataBytes();
                Assert.AreEqual<int>(expectedValue, numberOfDataBytes);
            };

            func(":020000020000FC", 2);
            func(":100000000C943E010C9430350C9431350C943235FF", 16);
            func(":00000001FF", 0);
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        public void GetNumberOfDataBytesInFact()
        {
            Action<string, int> func = (record, expectedValue) =>
            {
                var numberOfDataBytes = record.GetNumberOfDataBytesInFact();
                Assert.AreEqual<int>(expectedValue, numberOfDataBytes);
            };

            func(":020000020000FC", 2);
            func(":100000000C943E010C9430350C9431350C943235FF", 16);
            func(":00000001FF", 0);
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        public void GetAddress()
        {
            Action<string, long> func = (record, expectedValue) =>
            {
                long address = record.GetAddress();
                Assert.AreEqual<long>(expectedValue, address);
            };

            func(":020000021000EC", 0x10000);
            func(":10993000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF37", 0x9930);
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAddressException()
        {
            var record = ":00000001FF";
            try
            {
                var address = record.GetAddress();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        public void GetDataBytes()
        {
            var record = ":100000000C943E010C9430350C9431350C943235FF";
            var dataBytes = record.GetDataBytes();
            var expectedDataBytes = new byte[] { 0x0C, 0x94, 0x3E, 0x01, 0x0C, 0x94, 0x30, 0x35, 
                0x0C, 0x94, 0x31, 0x35, 0x0C, 0x94, 0x32, 0x35 };
            Assert.IsTrue(Enumerable.SequenceEqual(expectedDataBytes, dataBytes));  
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDataBytesException()
        {
            var record = ":00000001FF";
            try
            {
                var dataBytes = record.GetDataBytes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        public void GetCheckSum()
        {
            Action<string, int> func = (record, expectedValue) =>
            {
                var checkSum = record.GetCheckSum();
                Assert.AreEqual<int>(expectedValue, checkSum);
            };

            func(":020000020000FC", 0xFC);
            func(":100000000C943E010C9430350C9431350C943235FF", 0xFF);
            func(":00000001FF", 0xFF);
        }

        [TestMethod]
        [TestCategory("RecordExtensionMethods")]
        public void GetCheckSumInFact()
        {
            Action<string, int> func = (record, expectedValue) =>
            {
                var checkSum = record.GetCheckSumInFact();
                Assert.AreEqual<int>(expectedValue, checkSum);
            };

            func(":020000020000FC", 0xFC);
            func(":100000000C943E010C9430350C9431350C943235FF", 0xFF);
            func(":00000001FF", 0xFF);
        }
    }
}
