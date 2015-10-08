namespace Service.IO.Buffers.Tx
{
    /// <summary>
    /// Tx buffer in read fuse mode
    /// </summary>
    public class IOBufferReadFuseTx : IOBuffer
    {
        public IOBufferReadFuseTx() : base(IOBuffer.ReadFuseBufferTxSize) { }
    }
}
