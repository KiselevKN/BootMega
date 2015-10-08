namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in write page mode
    /// </summary>
    public class IOBufferWritePageRx : IOBuffer
    {
        public IOBufferWritePageRx() : base(IOBuffer.WritePageBufferRxSize) { }
    }
}
