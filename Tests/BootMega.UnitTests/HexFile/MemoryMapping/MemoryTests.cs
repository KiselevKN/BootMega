using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.HexFile.MemoryMapping;

namespace BootMega.UnitTests
{
    [TestClass]
    public class MemoryTests
    {
        [TestMethod]
        [TestCategory("Memory")]
        public void Create()
        {
            Memory memory = new Memory(0x1000);

            Assert.AreEqual<long>(0x1000, memory.Size);
            Assert.AreEqual(0x10, memory.NumberOfPages);
            Assert.AreEqual<bool>(false, memory.IsExtendedMemory);

            memory = new Memory(0x31000);

            Assert.AreEqual<long>(0x31000, memory.Size);
            Assert.AreEqual(0x310, memory.NumberOfPages);
            Assert.AreEqual<bool>(false, memory.IsExtendedMemory);

            memory = new Memory(0x1000000);

            Assert.AreEqual<long>(0x1000000, memory.Size);
            Assert.AreEqual(0x10000, memory.NumberOfPages);
            Assert.AreEqual<bool>(true, memory.IsExtendedMemory);
        }

        [TestMethod]
        [TestCategory("Memory")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateArgumentZeroException()
        {
            try
            {
                Memory memory = new Memory(0);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex); throw ex;
            }         
        }

        [TestMethod]
        [TestCategory("Memory")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateArgumentLargeSizeException()
        {
            try
            {
                Memory memory = new Memory(0x100000001);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex); throw ex;
            }        
        }

        [TestMethod]
        [TestCategory("Memory")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEx()
        {
            try
            {
                Memory memory = new Memory(0x1004);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex); throw ex;
            }
        }

        [TestMethod]
        [TestCategory("Memory")]
        public void Indexer()
        {
            Memory memory = new Memory(0x20000);

            memory[0] = 0;
            memory[0x1FFFF] = 0;

            Assert.IsFalse(memory.IsEmpty);
            Assert.AreEqual(2, memory.FillingFactor);
            Assert.AreEqual(0, memory[0]);
            Assert.AreEqual(0, memory[0x1FFFF]);
        }

        [TestMethod]
        [TestCategory("Memory")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerOutOfRange()
        {
            try
            {
                Memory memory = new Memory(0x1000);
                memory[0x1000] = 0;
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        [TestMethod]
        [TestCategory("Memory")]
        public void IsEmpty()
        {
            Memory memory = new Memory(0x1000);

            Assert.IsTrue(memory.IsEmpty);

            memory[256] = 0x00;
            memory[0] = 0x00;

            Assert.IsFalse(memory.IsEmpty);
        }

        [TestMethod]
        [TestCategory("Memory")]
        public void FillingFactor()
        {
            Memory memory = new Memory(0x1000);

            Assert.AreEqual(0, memory.FillingFactor);
            
            memory[256] = 0x00;
            memory[0] = 0x00;

            Assert.AreEqual(2, memory.FillingFactor);
        }

        [TestMethod]
        [TestCategory("Memory")]
        public void Clear()
        {
            Memory memory = new Memory(0x1000);

            memory[256] = 0x00;
            memory[0] = 0x00;

            Assert.AreEqual(2, memory.FillingFactor);

            memory.Clear();

            Assert.AreEqual(0, memory.FillingFactor);
        }

        [TestMethod]
        [TestCategory("Memory")]
        public void GetPage()
        {
            Memory memory = new Memory(0x1000);

            memory[0x300] = 0x22;
            memory[0x3FF] = 0xAA;

            Page page = memory.GetPage(3);

            Assert.AreEqual<byte>(0x22, page[0]);
            Assert.AreEqual<byte>(0xFF, page[1]);
            Assert.AreEqual<byte>(0xAA, page[255]);
        }

        [TestMethod]
        [TestCategory("Memory")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetPageOutOfRange()
        {
            Memory memory = new Memory(0x1000);
            Page page = memory.GetPage(0x10);
        }


        [TestMethod]
        [TestCategory("Memory")]
        public void GetPageByAddress()
        {
            Memory memory = new Memory(0x1000);

            memory[0x20] = 0x22;
            memory[0x11F] = 0xAA;

            memory[0xF00] = 0x22;
            memory[0xFFF] = 0xAA;

            Page page = memory.GetPageByAddress(0x20);

            Assert.AreEqual<byte>(0x22, page[0]);
            Assert.AreEqual<byte>(0xFF, page[1]);
            Assert.AreEqual<byte>(0xAA, page[255]);

            page = memory.GetPageByAddress(0xF00);

            Assert.AreEqual<byte>(0x22, page[0]);
            Assert.AreEqual<byte>(0xFF, page[1]);
            Assert.AreEqual<byte>(0xAA, page[255]);
        }

        [TestMethod]
        [TestCategory("Memory")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetPageByAddressOutOfRange()
        {
            Memory memory = new Memory(0x1000);

            Page page = memory.GetPageByAddress(0xF01);
        }

        [TestMethod]
        [TestCategory("Memory")]
        public void Equals()
        {
            Memory memory1 = new Memory(0x1000);
            memory1[76] = 0x34;

            Memory memory2 = new Memory(0x1000);
            memory2[76] = 0x34;

            Assert.IsTrue(memory1.Equals(memory1));
            Assert.IsTrue(memory1.Equals(memory2));

            List<long> differences;
            Assert.IsTrue(Memory.Equals(memory1, memory2, out differences));
            Assert.AreEqual(0, differences.Count);

            memory2[76] = 0x89;

            Assert.IsFalse(memory1.Equals(memory2));
            Assert.IsFalse(Memory.Equals(memory1, memory2, out differences));
            Assert.AreEqual(1, differences.Count);
            Assert.AreEqual(76, differences[0]);
        }
    }
}
