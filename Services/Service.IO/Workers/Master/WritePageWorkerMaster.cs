using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using Microsoft.Practices.Prism.Logging;
using Service.HexFile.MemoryMapping;
using Service.IO.Buffers.Rx;
using Service.IO.Protocols;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;
using Service.IO.TaskManager;
using Service.Settings.Enums;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    public class WritePageWorkerMaster : IOWorkerBase<double, bool>
    {
        #region fields

        private IMemory memory;
        private MemoryType memoryType;
        private Processor processor;

        #endregion

        #region ctors

        public WritePageWorkerMaster(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger, 
            MemoryType memoryType, Processor processor, IMemory memory)
            : base(port, serialPortSettings, logger)
        {
            this.memory = memory;
            this.memoryType = memoryType;
            this.processor = processor;
        }

        #endregion

        #region methods

        public override bool Run(IInTaskManager<double> taskManager)
        {
            Mode mode = (memoryType == MemoryType.FLASH) ? Mode.WriteFlashPage : Mode.WriteEepromPage;

            var txProtocol = new ProtocolWritePageTx(serialPortSettings.HeaderTX, mode);
            var rxProtocol = new ProtocolWritePageRx(serialPortSettings.HeaderRX, mode);
            var bufferRx = new IOBufferWritePageRx();

            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits, 50, SerialPort.InfiniteTimeout))
            {
                var nonEmptyPages = GetNonEmptyPagesWithNumber();

                Stopwatch stopWatch = new Stopwatch();
                int pageCount = 0;
                foreach (var page in nonEmptyPages)
                {
                    for (int pageLineNumber = 0; pageLineNumber < Page.NumberOfLines; pageLineNumber++)
                    {
                        txProtocol.PageNumber = page.Key;
                        txProtocol.PageLineNumber = pageLineNumber;
                        txProtocol.Line = page.Value.GetLine(pageLineNumber);
                        for (;;)
                        {
                            Transmit(sp, txProtocol, taskManager.SynchronizationContext);
                            RestartReceiv();
                            stopWatch.Restart();
                            while (stopWatch.Elapsed < TimeSpan.FromMilliseconds(100))
                            {
                                taskManager.IfCancellation();
                                if (IfReceived(sp, rxProtocol, bufferRx, taskManager.SynchronizationContext))
                                {
                                    if (rxProtocol.PageNumber == page.Key)
                                    {
                                        if (rxProtocol.PageLineNumber == pageLineNumber)
                                        {
                                            goto label1;
                                        }
                                        else
                                            RestartReceiv();
                                    }
                                    else
                                        RestartReceiv();
                                }
                            }
                        }
                    label1:;
                    }
                    taskManager.OnProgress((double)pageCount++ / (nonEmptyPages.Count - 1) * 100.00);
                }
                Thread.Sleep(1000);
                return true;
            }
        }

        private Dictionary<int, Page> GetNonEmptyPagesWithNumber()
        {
            var nonEmptyPages = new Dictionary<int,Page>();

            int numberOfPages = (memoryType == MemoryType.FLASH)
                ? (int)processor.BootStartAddress / Page.Size
                : (int)processor.EepromSize / Page.Size;

            for (int i = 0; i < numberOfPages; i++)
            {
                var page = memory.GetPage(i);
                if (!page.IsEmpty)
                    nonEmptyPages[i] = page;
            }

            return nonEmptyPages;
        }

        #endregion
    }
}
