using System;
using System.IO.Ports;

namespace Service.IO
{
    public interface ISerialPortManager : IDisposable
    {
        bool IsOpened { get; }
        void Open();
        void Write(byte[] b);
        bool ReadByte(out byte b);
        void Close();
        event EventHandler<SerialErrorReceivedEventArgs> ErrorReceived;
    }
}
