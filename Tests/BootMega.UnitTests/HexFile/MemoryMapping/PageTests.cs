using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.HexFile.MemoryMapping;

namespace BootMega.UnitTests
{
    [TestClass]
    public class PageTests
    {
        [TestMethod]
        [TestCategory("Page")]
        public void Indexer()
        {
            Page page = new Page();

            for (int i = 0; i < Page.Size; i++)
                Assert.AreEqual<byte>(0, page[i]);

            page[0] = 0x22;
            page[255] = 0x33;

            Assert.AreEqual<byte>(0x22, page[0]);
            Assert.AreEqual<byte>(0x33, page[255]);
        }

        [TestMethod]
        [TestCategory("Page")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerOutOfRange()
        {
            Page page = new Page();
            page[256] = 0xFF;
        }

        [TestMethod]
        [TestCategory("Page")]
        public void IsEmpty()
        {
            Page page = new Page();

            Assert.IsFalse(page.IsEmpty);

            for (int i = 0; i < Page.Size; i++)
                page[i] = Page.EmptyCell;

            Assert.IsTrue(page.IsEmpty);
        }

        [TestMethod]
        [TestCategory("Page")]
        public void GetLine()
        {
            Page page = new Page();
            
            page[250] = 0x33;

            PageLine line = page.GetLine(15);

            for (int i = 0; i < PageLine.Size; i++)
                Assert.AreEqual<byte>((byte)((i == 10) ? 0x33 : 0), line[i]);
        }

        [TestMethod]
        [TestCategory("Page")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetLineOutOfRange()
        {
            Page page = new Page();
            PageLine line = page.GetLine(16);
        }


        [TestMethod]
        [TestCategory("Page")]
        public void SetLine()
        {
            Page page = new Page();
            PageLine line = new PageLine();
            line[0] = 0x22;
            line[15] = 0xAA;

            page.SetLine(0, line);
            page.SetLine(15, line);

            Assert.AreEqual<byte>(0x22, page[0]);
            Assert.AreEqual<byte>(0xAA, page[15]);
            Assert.AreEqual<byte>(0x22, page[240]);
            Assert.AreEqual<byte>(0xAA, page[255]);
        }

        [TestMethod]
        [TestCategory("Page")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetLineOutOfRange()
        {
            Page page = new Page();
            PageLine line = new PageLine();
            page.SetLine(16, line);
        }

        [TestMethod]
        [TestCategory("Page")]
        public void ToByteArray()
        {
            Page page = new Page();

            page[0] = 0x44;
            page[255] = 0x66;

            byte[] array = page;

            Assert.AreEqual<int>(Page.Size, array.Length);
            Assert.AreEqual<byte>(0x44, array[0]);
            Assert.AreEqual<byte>(0, array[1]);
            Assert.AreEqual<byte>(0x66, array[255]);
        }
    }
}
