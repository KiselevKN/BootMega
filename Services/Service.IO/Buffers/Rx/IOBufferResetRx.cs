namespace Service.IO.Buffers.Rx
{
    /// <summary>
    /// Rx buffer in reset mode
    /// </summary>
    public class IOBufferResetRx : IOBuffer
    {
        public IOBufferResetRx() : base(IOBuffer.ResetBufferRxSize) { }
    }
}
