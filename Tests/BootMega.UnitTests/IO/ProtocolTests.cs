using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.IO.Protocols;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;

namespace BootMega.UnitTests.IO
{
    [TestClass]
    public class ProtocolTests
    {
        [TestMethod]
        [TestCategory("Protocol")]
        public void Connection()
        {
            ProtocolConnectionTx ctx = new ProtocolConnectionTx(0x80);
            ProtocolConnectionRx crx = new ProtocolConnectionRx(0x81);

            var buf = ctx.Pack();
            Assert.IsTrue(ctx.UnPack(buf));
            Console.WriteLine(buf.ToString());

            buf = crx.Pack();
            Assert.IsTrue(crx.UnPack(buf));
            Console.WriteLine(buf.ToString());
        }

        [TestMethod]
        [TestCategory("Protocol")]
        public void EraseFlash()
        {
            var ctx1 = new ProtocolErasePageTx(0x80, Mode.EraseFlashPage);
            ctx1.PageNumber = 0xFFFFFF;
            var ctx2 = new ProtocolErasePageTx(0x80, Mode.EraseFlashPage);

            var buf = ctx1.Pack();
            Assert.IsTrue(ctx2.UnPack(buf));
            Assert.AreEqual(0xFFFFFF, ctx2.PageNumber);
            Console.WriteLine(buf.ToString());

            var crx1 = new ProtocolErasePageRx(0x81, Mode.EraseFlashPage);
            crx1.PageNumber = 0x7FFF;
            var crx2 = new ProtocolErasePageRx(0x81, Mode.EraseFlashPage);

            buf = crx1.Pack();
            Assert.IsTrue(crx2.UnPack(buf));
            Assert.AreEqual(0x7FFF, crx2.PageNumber);
            Console.WriteLine(buf.ToString());
        }

        [TestMethod]
        [TestCategory("Protocol")]
        public void IsEmptyPageFlash()
        {
            var ctx1 = new ProtocolIsEmptyPageTx(0x80, Mode.IsEmptyFlashPage);
            ctx1.PageNumber = 0xFFFFFF;
            var ctx2 = new ProtocolIsEmptyPageTx(0x80, Mode.IsEmptyFlashPage);

            var buf = ctx1.Pack();
            Assert.IsTrue(ctx2.UnPack(buf));
            Assert.AreEqual(0xFFFFFF, ctx2.PageNumber);
            Console.WriteLine(buf.ToString());

            var crx1 = new ProtocolIsEmptyPageRx(0x81, Mode.IsEmptyFlashPage);
            crx1.PageNumber = 0x7FFF;
            crx1.IsEmpty = true;
            var crx2 = new ProtocolIsEmptyPageRx(0x81, Mode.IsEmptyFlashPage);

            buf = crx1.Pack();
            Assert.IsTrue(crx2.UnPack(buf));
            Assert.AreEqual(0x7FFF, crx2.PageNumber);
            Assert.IsTrue(crx2.IsEmpty);
            Console.WriteLine(buf.ToString());
        }

        [TestMethod]
        [TestCategory("Protocol")]
        public void ReadFlash()
        {
            var ctx1 = new ProtocolReadPageTx(0x80, Mode.ReadFlashPage);
            ctx1.PageNumber = 0xFFFFFF;
            ctx1.PageLineNumber = 15;
            var ctx2 = new ProtocolReadPageTx(0x80, Mode.ReadFlashPage);

            var buf = ctx1.Pack();
            Assert.IsTrue(ctx2.UnPack(buf));
            Assert.AreEqual(0xFFFFFF, ctx2.PageNumber);
            Assert.AreEqual(15, ctx2.PageLineNumber);
            Console.WriteLine(buf.ToString());

            var crx1 = new ProtocolReadPageRx(0x81, Mode.ReadFlashPage);
            crx1.PageNumber = 0x7FFF;
            crx1.PageLineNumber = 1;
            crx1.Line[0] = 0xE4;
            crx1.Line[7] = 0xFE;
            crx1.Line[15] = 0x45;
            var crx2 = new ProtocolReadPageRx(0x81, Mode.ReadFlashPage);

            buf = crx1.Pack();
            Assert.IsTrue(crx2.UnPack(buf));
            Assert.AreEqual(0x7FFF, crx2.PageNumber);
            Assert.AreEqual(1, crx2.PageLineNumber);
            Assert.AreEqual<byte>(0xE4, crx2.Line[0]);
            Assert.AreEqual<byte>(0xFE, crx2.Line[7]);
            Assert.AreEqual<byte>(0x45, crx2.Line[15]);
            Console.WriteLine(buf.ToString());
        }

        [TestMethod]
        [TestCategory("Protocol")]
        public void WriteFlash()
        {
            var ctx1 = new ProtocolWritePageRx(0x80, Mode.WriteFlashPage);
            ctx1.PageNumber = 0xFFFFFF;
            ctx1.PageLineNumber = 15;
            var ctx2 = new ProtocolWritePageRx(0x80, Mode.WriteFlashPage);

            var buf = ctx1.Pack();
            Assert.IsTrue(ctx2.UnPack(buf));
            Assert.AreEqual(0xFFFFFF, ctx2.PageNumber);
            Assert.AreEqual(15, ctx2.PageLineNumber);
            Console.WriteLine(buf.ToString());

            var crx1 = new ProtocolWritePageTx(0x81, Mode.WriteFlashPage);
            crx1.PageNumber = 0x7FFF;
            crx1.PageLineNumber = 1;
            crx1.Line[0] = 0xE4;
            crx1.Line[7] = 0xFE;
            crx1.Line[15] = 0x45;
            var crx2 = new ProtocolWritePageTx(0x81, Mode.WriteFlashPage);

            buf = crx1.Pack();
            Assert.IsTrue(crx2.UnPack(buf));
            Assert.AreEqual(0x7FFF, crx2.PageNumber);
            Assert.AreEqual(1, crx2.PageLineNumber);
            Assert.AreEqual<byte>(0xE4, crx2.Line[0]);
            Assert.AreEqual<byte>(0xFE, crx2.Line[7]);
            Assert.AreEqual<byte>(0x45, crx2.Line[15]);
            Console.WriteLine(buf.ToString());
        }
    }
}
