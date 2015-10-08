using System;
using System.IO.Ports;

namespace Service.IO
{
    public class SerialPortManager : ISerialPortManager
    {
        #region fields

        private SerialPort serialPort;

        #endregion

        #region ctors

        public SerialPortManager(string portName, int baudRate, Parity parity, StopBits stopBits,
            int readTimeout, int writeTimeout,  int dataBits = 8)
        {
            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.Parity = parity;
            serialPort.StopBits = stopBits;
            serialPort.DataBits = dataBits;
            serialPort.Handshake = Handshake.None;
            serialPort.ReadTimeout = readTimeout;
            serialPort.WriteTimeout = writeTimeout;
            serialPort.ReadBufferSize = 256;
            serialPort.WriteBufferSize = 256;
            serialPort.ErrorReceived += serialPort_ErrorReceived;
            serialPort.Open();
        }

        #endregion

        #region methods

        void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            OnErrorReceived(sender, e);
        }

        #endregion

        #region ISerialPortManager Members

        public void Open()
        {
            if (!serialPort.IsOpen)
                serialPort.Open();
        }

        public void Write(byte[] b)
        {
            serialPort.DiscardInBuffer();
            serialPort.DiscardOutBuffer();
            serialPort.Write(b, 0, b.Length);
        }

        public bool ReadByte(out byte b)
        {
            int result;

            try { result = serialPort.ReadByte(); }
            catch(TimeoutException)
            {
                result = -1;
            }

            b = (byte)result;
            return (result == -1) ? false : true;
        }

        public void Close()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        public bool IsOpened
        {
            get { return serialPort.IsOpen; }
        }

        public event EventHandler<SerialErrorReceivedEventArgs> ErrorReceived;


        protected virtual void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            var handler = ErrorReceived;
            if (handler != null)
                    handler(sender, e);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (serialPort != null)
            {
                serialPort.ErrorReceived -= serialPort_ErrorReceived;
                serialPort.Dispose();
            }
        }

        #endregion
    }
}
