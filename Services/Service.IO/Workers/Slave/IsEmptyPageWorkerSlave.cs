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
    public class IsEmptyPageWorkerSlave : IOWorkerBase<bool>
    {
        #region fields

        private IMemory memory;
        private MemoryType memoryType;

        #endregion

        #region ctors

        public IsEmptyPageWorkerSlave(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger, 
            MemoryType memoryType, IMemory memory)
            : base(port, serialPortSettings, logger)
        {
            this.memory = memory;
            this.memoryType = memoryType;
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager taskManager)
        {
            Mode mode = (memoryType == MemoryType.FLASH) ? Mode.IsEmptyFlashPage : Mode.IsEmptyEepromPage;

            var protocolTx = new ProtocolIsEmptyPageTx(serialPortSettings.HeaderTX, mode);
            var protocolRx = new ProtocolIsEmptyPageRx(serialPortSettings.HeaderRX, mode);
            var bufferTx = new IOBufferIsEmptyPageTx();

            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits, 50, SerialPort.InfiniteTimeout))
            {
                for (;;)
                {
                    taskManager.IfCancellation();
                    if (IfReceived(sp, protocolTx, bufferTx, taskManager.SynchronizationContext))
                    {
                        protocolRx.PageNumber = protocolTx.PageNumber;
                        protocolRx.IsEmpty = memory.GetPage(protocolTx.PageNumber).IsEmpty;
                        Transmit(sp, protocolRx, taskManager.SynchronizationContext);
                        RestartReceiv();
                    }
                }
            }
        }

        #endregion
    }
}
