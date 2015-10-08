using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.HexFile.MemoryMapping;

namespace BootMega.UnitTests
{
    [TestClass]
    public class PageLineTests
    {
        [TestMethod]
        [TestCategory("PageLine")]
        public void Indexer()
        {
            PageLine line = new PageLine();

            line[0] = 0x44;
            line[15] = 0x66;

            Assert.AreEqual<byte>(0x44, line[0]);
            Assert.AreEqual<byte>(0x66, line[15]);
            Assert.AreEqual<byte>(0x00, line[1]);
        }

        [TestMethod]
        [TestCategory("PageLine")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerOutOfRange()
        {
            PageLine line = new PageLine();
            line[16] = 0x44;
        }

        [TestMethod]
        [TestCategory("PageLine")]
        public void ToByteArray()
        {
            PageLine line = new PageLine();

            line[0] = 0x44;
            line[15] = 0x66;

            byte[] array = line;

            Assert.AreEqual<int>(PageLine.Size, array.Length);
            Assert.AreEqual<byte>(0x44, array[0]);
            Assert.AreEqual<byte>(0, array[1]);
            Assert.AreEqual<byte>(0x66, array[15]);
        }

        [TestMethod]
        [TestCategory("PageLine")]
        public void ToFormattedString()
        {
            PageLine line = new PageLine();

            for (int i = 0; i < PageLine.Size; i++)
                line[i] = (byte)(0x30 + i);

            Console.WriteLine(line.ToString());
            Console.WriteLine();
            Console.WriteLine(line.ToString("G"));
            Console.WriteLine();
            Console.WriteLine(line.ToString("H"));
            Console.WriteLine();
            Console.WriteLine(line.ToString("C"));
            Console.WriteLine();
        }

        [TestMethod]
        [TestCategory("PageLine")]
        [ExpectedException(typeof(FormatException))]
        public void ToStringInvalidFormat()
        {
            try
            {
                PageLine line = new PageLine();
                Console.WriteLine(line.ToString("X"));
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
