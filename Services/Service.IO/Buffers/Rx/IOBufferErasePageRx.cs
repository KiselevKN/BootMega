namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in erase page mode
    /// </summary>
    public class IOBufferErasePageRx : IOBuffer
    {
        public IOBufferErasePageRx() : base(IOBuffer.ErasePageBufferRxSize) { }
    }
}
