using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.IO.Buffers;
using Service.IO.Buffers.Rx;
using Service.IO.Buffers.Tx;

namespace BootMega.UnitTests
{
    [TestClass]
    public class BufferTests
    {
        [TestMethod]
        [TestCategory("Buffer")]
        public void Create()
        {
            Assert.AreEqual(IOBuffer.ConnectionBufferTxSize, (new IOBufferConnectionTx()).Size);
            Assert.AreEqual(IOBuffer.ConnectionBufferRxSize, (new IOBufferConnectionRx()).Size);

            Assert.AreEqual(IOBuffer.WritePageBufferTxSize, (new IOBufferWritePageTx()).Size);
            Assert.AreEqual(IOBuffer.WritePageBufferRxSize, (new IOBufferWritePageRx()).Size);

            Assert.AreEqual(IOBuffer.ReadPageBufferTxSize, (new IOBufferReadPageTx()).Size);
            Assert.AreEqual(IOBuffer.ReadPageBufferRxSize, (new IOBufferReadPageRx()).Size);

            Assert.AreEqual(IOBuffer.ErasePageBufferTxSize, (new IOBufferErasePageTx()).Size);
            Assert.AreEqual(IOBuffer.ErasePageBufferRxSize, (new IOBufferErasePageRx()).Size);

            Assert.AreEqual(IOBuffer.ReadFuseBufferTxSize, (new IOBufferReadFuseTx()).Size);
            Assert.AreEqual(IOBuffer.ReadFuseBufferRxSize, (new IOBufferReadFuseRx()).Size);

            Assert.AreEqual(IOBuffer.ReadLockBufferTxSize, (new IOBufferReadLockTx()).Size);
            Assert.AreEqual(IOBuffer.ReadLockBufferRxSize, (new IOBufferReadLockRx()).Size);

            Assert.AreEqual(IOBuffer.ResetBufferTxSize, (new IOBufferResetTx()).Size);
            Assert.AreEqual(IOBuffer.ResetBufferRxSize, (new IOBufferResetRx()).Size);

            Assert.AreEqual(IOBuffer.IsEmptyPageBufferTxSize, (new IOBufferIsEmptyPageTx()).Size);
            Assert.AreEqual(IOBuffer.IsEmptyPageBufferRxSize, (new IOBufferIsEmptyPageRx()).Size);
        }

        [TestMethod]
        [TestCategory("Buffer")]
        public void Indexer()
        {
            IOBuffer buffer = new IOBufferConnectionTx();
            buffer[2] = 9;
            Assert.AreEqual<byte>(9, buffer[2]);
        }

        [TestMethod]
        [TestCategory("Buffer")]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerOutOfRange()
        {
            IOBuffer buffer = new IOBufferConnectionTx();
            buffer[100] = 9;
        }

        [TestMethod]
        [TestCategory("Buffer")]
        public void ToByteArray()
        {
            IOBuffer buffer = new IOBufferConnectionTx();

            buffer[0] = 0x22;
            buffer[1] = 0x33;

            byte[] array = buffer;

            Assert.AreEqual<int>(buffer.Size, array.Length);
            Assert.AreEqual<byte>(0x22, array[0]);
            Assert.AreEqual<byte>(0x33, array[1]);
        }

        [TestMethod]
        [TestCategory("Buffer")]
        public void ToHexString()
        {
            IOBuffer buffer = new IOBufferConnectionTx();

            buffer[0] = 0x22;
            buffer[buffer.Size - 1] = 0x33;

            Console.WriteLine(buffer.ToString());
        }
    }
}
