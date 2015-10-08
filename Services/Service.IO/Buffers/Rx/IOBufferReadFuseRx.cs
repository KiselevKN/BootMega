namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in read fuse mode
    /// </summary>
    public class IOBufferReadFuseRx : IOBuffer
    {
        public IOBufferReadFuseRx() : base(IOBuffer.ReadFuseBufferRxSize) { }
    }
}
