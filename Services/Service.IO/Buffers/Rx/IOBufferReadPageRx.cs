namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in read page mode
    /// </summary>
    public class IOBufferReadPageRx : IOBuffer
    {
        public IOBufferReadPageRx() : base(IOBuffer.ReadPageBufferRxSize) { }
    }
}
