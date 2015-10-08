using System.IO.Ports;
using Microsoft.Practices.Prism.Logging;
using Service.HexFile.MemoryMapping;
using Service.IO.Buffers.Tx;
using Service.IO.Protocols;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;
using Service.IO.TaskManager;
using Service.Settings.Enums;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    public class ErasePageWorkerSlave : IOWorkerBase<bool>
    {
        #region fields

        private MemoryType memoryType;
        private IMemory memory;

        #endregion

        #region ctors

        public ErasePageWorkerSlave(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger,
            MemoryType memoryType, IMemory memory) : 
            base(port, serialPortSettings, logger) 
        {
            this.memoryType = memoryType;
            this.memory = memory;
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager taskManager)
        {
            Mode mode = (memoryType == MemoryType.FLASH) ? Mode.EraseFlashPage : Mode.EraseEepromPage;

            var protocolTx = new ProtocolErasePageTx(serialPortSettings.HeaderTX, mode);
            var protocolRx = new ProtocolErasePageRx(serialPortSettings.HeaderRX, mode);
            var bufferTx = new IOBufferErasePageTx();

            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits, 50, SerialPort.InfiniteTimeout))
            {
                for (;;)
                {
                    taskManager.IfCancellation();
                    if (IfReceived(sp, protocolTx, bufferTx, taskManager.SynchronizationContext))
                    {
                        protocolRx.PageNumber = protocolTx.PageNumber;
                        memory.SetPage(protocolTx.PageNumber, new Page());
                        Transmit(sp, protocolRx, taskManager.SynchronizationContext);
                        RestartReceiv();
                    }
                }
            }
        }

        #endregion
    }
}
