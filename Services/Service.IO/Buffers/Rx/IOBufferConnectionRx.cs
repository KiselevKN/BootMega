namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in connection mode
    /// </summary>
    public class IOBufferConnectionRx : IOBuffer
    {
        public IOBufferConnectionRx() : base(IOBuffer.ConnectionBufferRxSize) { }
    }
}
