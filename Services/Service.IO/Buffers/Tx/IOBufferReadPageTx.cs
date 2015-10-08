namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in read page mode
    /// </summary>
    public class IOBufferReadPageTx : IOBuffer
    {
        public IOBufferReadPageTx() : base(IOBuffer.ReadPageBufferTxSize) { }
    }
}
