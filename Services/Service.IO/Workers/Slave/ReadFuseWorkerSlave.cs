using System.IO.Ports;
using Microsoft.Practices.Prism.Logging;
using Service.IO.Buffers.Tx;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;
using Service.IO.TaskManager;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    public class ReadFuseWorkerSlave : IOWorkerBase<bool>
    {
        #region fields

        private byte[] fuses;
        
        #endregion

        #region ctors

        public ReadFuseWorkerSlave(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger, byte[] fuses) : 
            base(port, serialPortSettings, logger) 
        { 
            this.fuses = fuses;
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager taskManager)
        {
            var protocolTx = new ProtocolReadFuseTx(serialPortSettings.HeaderTX);
            var protocolRx = new ProtocolReadFuseRx(serialPortSettings.HeaderRX);
            var bufferTx = new IOBufferReadFuseTx();

            protocolRx.ExtendedFuses = fuses[0];
            protocolRx.HightFuses = fuses[1];
            protocolRx.LowFuses = fuses[2];

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
