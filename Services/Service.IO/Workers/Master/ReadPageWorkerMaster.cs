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
    public class ReadPageWorkerMaster : IOWorkerBase<double, IMemory>
    {
        #region fields

        private List<int> numberOfPages;
        private MemoryType memoryType;
        private Processor processor;

        #endregion

        #region ctors

        public ReadPageWorkerMaster(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger, 
            MemoryType memoryType, Processor processor, List<int> numberOfPages)
            : base(port, serialPortSettings, logger)
        {
            this.numberOfPages = numberOfPages;
            this.memoryType = memoryType;
            this.processor = processor;
        }

        #endregion

        #region methods

        public override IMemory Run(IInTaskManager<double> taskManager)
        {
            Mode mode = (memoryType == MemoryType.FLASH) ? Mode.ReadFlashPage : Mode.ReadEepromPage;

            var txProtocol = new ProtocolReadPageTx(serialPortSettings.HeaderTX, mode);
            var rxProtocol = new ProtocolReadPageRx(serialPortSettings.HeaderRX, mode);
            var bufferRx = new IOBufferReadPageRx();

            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits,
                50, SerialPort.InfiniteTimeout))
            {
                IMemory memory = new Memory((memoryType == MemoryType.FLASH) ? processor.FlashSize : processor.EepromSize);
                Stopwatch stopWatch = new Stopwatch();
                int pageCounter = 0;
                foreach(var pageNumber in numberOfPages)
                {
                    Page page = new Page();
                    txProtocol.PageNumber = pageNumber;
                    for (int pageLineNumber = 0; pageLineNumber < Page.NumberOfLines; pageLineNumber++)
                    {
                        txProtocol.PageLineNumber = pageLineNumber;                       
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
                                    if (rxProtocol.PageNumber == pageNumber)
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
                    label1:
                        page.SetLine(pageLineNumber, rxProtocol.Line);
                    }
                    memory.SetPage(pageNumber, page);
                    pageCounter++;
                    taskManager.OnProgress((double)pageCounter / numberOfPages.Count * 100.00);
                }
                Thread.Sleep(1000);
                return memory;
            }
        }

        #endregion
    }
}
