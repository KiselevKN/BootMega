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
    public class WritePageWorkerSlave : IOWorkerBase<bool>
    {
        #region fields

        private MemoryType memoryType;
        private IMemory memory;

        #endregion

        #region ctors

        public WritePageWorkerSlave(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger,
            MemoryType memoryType, IMemory memory) : base(port, serialPortSettings, logger) 
        {
            this.memoryType = memoryType;
            this.memory = memory;
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager taskManager)
        {
            Mode mode = (memoryType == MemoryType.FLASH) ? Mode.WriteFlashPage : Mode.WriteEepromPage;

            var protocolTx = new ProtocolWritePageTx(serialPortSettings.HeaderTX, mode);
            var protocolRx = new ProtocolWritePageRx(serialPortSettings.HeaderRX, mode);
            var bufferTx = new IOBufferWritePageTx();

            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits, 50, SerialPort.InfiniteTimeout))
            {
                for (;;)
                {
                    taskManager.IfCancellation();
                    if (IfReceived(sp, protocolTx, bufferTx, taskManager.SynchronizationContext))
                    {
                        protocolRx.PageNumber = protocolTx.PageNumber;
                        protocolRx.PageLineNumber = protocolTx.PageLineNumber;

                        var page = memory.GetPage(protocolTx.PageNumber);
                        page.SetLine(protocolTx.PageLineNumber,protocolTx.Line);
                        memory.SetPage(protocolTx.PageNumber, page);

                        Transmit(sp, protocolRx, taskManager.SynchronizationContext);
                    }
                }
            }
        }

        #endregion
    }
}
