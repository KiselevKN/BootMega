namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in is empty page mode
    /// </summary>
    public class IOBufferIsEmptyPageRx : IOBuffer
    {
        public IOBufferIsEmptyPageRx() : base(IOBuffer.IsEmptyPageBufferRxSize) { }
    }
}
