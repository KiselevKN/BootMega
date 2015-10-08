namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in reset mode
    /// </summary>
    public class IOBufferResetTx : IOBuffer
    {
        public IOBufferResetTx() : base(IOBuffer.ResetBufferTxSize) { }
    }
}
