using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
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
    public class IsEmptyPageWorkerMaster : IOWorkerBase<double, List<int>>
    {
        #region fields

        private MemoryType memoryType;
        private Processor processor;

        #endregion

        #region ctors

        public IsEmptyPageWorkerMaster(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger, 
            MemoryType memoryType, Processor processor) : base(port, serialPortSettings, logger) 
        {
            this.memoryType = memoryType;
            this.processor = processor;
        }

        #endregion

        #region methods

        public override List<int> Run(IInTaskManager<double> taskManager)
        {
            Mode mode = (memoryType == MemoryType.FLASH) ? Mode.IsEmptyFlashPage : Mode.IsEmptyEepromPage;

            var txProtocol = new ProtocolIsEmptyPageTx(serialPortSettings.HeaderTX, mode);
            var rxProtocol = new ProtocolIsEmptyPageRx(serialPortSettings.HeaderRX, mode);
            var bufferRx = new IOBufferIsEmptyPageRx();

            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits,
                50, SerialPort.InfiniteTimeout))
            {
                List<int> notEmptyPages = new List<int>();
                int numberOfPages = GetNumberOfPages();              
                int pageCounter = 0;

                Stopwatch stopWatch = new Stopwatch();
                for (;;)
                {
                label1:
                    if (pageCounter == numberOfPages)
                        return notEmptyPages;
                    
                    txProtocol.PageNumber = pageCounter;
                    Transmit(sp, txProtocol, taskManager.SynchronizationContext);
                    RestartReceiv();
                    stopWatch.Restart();
                    while (stopWatch.Elapsed < TimeSpan.FromMilliseconds(100))
                    {
                        taskManager.IfCancellation();
                        if (IfReceived(sp, rxProtocol, bufferRx, taskManager.SynchronizationContext))
                        {
                            if (rxProtocol.PageNumber == pageCounter)
                            {
                                if (!rxProtocol.IsEmpty)
                                    notEmptyPages.Add(rxProtocol.PageNumber);
                                pageCounter++;
                                taskManager.OnProgress((double)pageCounter / numberOfPages * 100.00);
                                goto label1;
                            }
                            else
                                RestartReceiv();
                        }
                    }
                }
            }
        }

        private int GetNumberOfPages()
        {
            return (memoryType == MemoryType.FLASH)
                ? (int)processor.BootStartAddress / Page.Size
                : (int)processor.EepromSize / Page.Size;
        }

        #endregion
    }
}
