namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in read lock mode
    /// </summary>
    public class IOBufferReadLockTx : IOBuffer
    {
        public IOBufferReadLockTx() : base(IOBuffer.ReadLockBufferTxSize) { }
    }
}
