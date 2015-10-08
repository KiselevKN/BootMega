namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in read lock mode
    /// </summary>
    public class IOBufferReadLockRx : IOBuffer
    {
        public IOBufferReadLockRx() : base(IOBuffer.ReadLockBufferRxSize) { }
    }
}
