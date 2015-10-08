namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in is empty page mode
    /// </summary>
    public class IOBufferIsEmptyPageTx : IOBuffer
    {
        public IOBufferIsEmptyPageTx() : base(IOBuffer.IsEmptyPageBufferTxSize) { }
    }
}
