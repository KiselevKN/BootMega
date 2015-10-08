namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in erase page mode
    /// </summary>
    public class IOBufferErasePageTx : IOBuffer
    {
        public IOBufferErasePageTx() : base(IOBuffer.ErasePageBufferTxSize) { }
    }
}
