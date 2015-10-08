using System;
using System.IO.Ports;
using Microsoft.Practices.Prism.Logging;
using Service.IO.Buffers.Tx;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;
using Service.IO.TaskManager;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    public class ConnectionWorkerSlave : IOWorkerBase<bool>
    {
        #region fields

        private int version;
        private DateTime date;
        #endregion

        #region ctors

        public ConnectionWorkerSlave(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger, 
            int version, DateTime date) :  base(port, serialPortSettings, logger) 
        {
            this.version = version;
            this.date = date;
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager taskManager)
        {
            var protocolTx = new ProtocolConnectionTx(serialPortSettings.HeaderTX);
            var protocolRx = new ProtocolConnectionRx(serialPortSettings.HeaderRX);
            var bufferTx = new IOBufferConnectionTx();
            
            protocolRx.Version = version;
            protocolRx.Date = date;
            
            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits, 50, SerialPort.InfiniteTimeout))
            {
                for (;;)
                {
                    taskManager.IfCancellation();
                    if (IfReceived(sp, protocolTx, bufferTx, taskManager.SynchronizationContext))
                    {
                        Transmit(sp, protocolRx, taskManager.SynchronizationContext);
                        RestartReceiv();
                    }
                }
            }
        }

        #endregion
    }
}
