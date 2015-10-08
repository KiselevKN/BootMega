using System.IO.Ports;
using Microsoft.Practices.Prism.Logging;
using Service.IO.Buffers.Tx;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;
using Service.IO.TaskManager;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    public class ResetWorkerSlave : IOWorkerBase<bool>
    {
        #region ctors

        public ResetWorkerSlave(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger) 
            :  base(port, serialPortSettings, logger) 
        {
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager taskManager)
        {
            var protocolTx = new ProtocolResetTx(serialPortSettings.HeaderTX);
            var protocolRx = new ProtocolResetRx(serialPortSettings.HeaderRX);
            var bufferTx = new IOBufferResetTx();


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
