namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in connection mode
    /// </summary>
    public class IOBufferConnectionTx : IOBuffer
    {
        public IOBufferConnectionTx() : base(IOBuffer.ConnectionBufferTxSize) { }
    }
}
