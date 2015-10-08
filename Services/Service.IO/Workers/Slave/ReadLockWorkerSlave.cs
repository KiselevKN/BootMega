using System.IO.Ports;
using Microsoft.Practices.Prism.Logging;
using Service.IO.Buffers.Tx;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;
using Service.IO.TaskManager;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    public class ReadLockWorkerSlave : IOWorkerBase<bool>
    {
        #region fields

        private byte locks;

        #endregion

        #region ctors

        public ReadLockWorkerSlave(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger, byte locks) :
            base(port, serialPortSettings, logger)
        {
            this.locks = locks;
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager taskManager)
        {
            var protocolTx = new ProtocolReadLockTx(serialPortSettings.HeaderTX);
            var protocolRx = new ProtocolReadLockRx(serialPortSettings.HeaderRX);
            var bufferTx = new IOBufferReadLockTx();

            protocolRx.Lock = locks;

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
