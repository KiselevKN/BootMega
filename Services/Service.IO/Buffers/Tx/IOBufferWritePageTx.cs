namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in write page mode
    /// </summary>
    public class IOBufferWritePageTx : IOBuffer
    {
        public IOBufferWritePageTx() : base(IOBuffer.WritePageBufferTxSize) { }
    }
}
