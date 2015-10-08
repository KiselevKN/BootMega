﻿using System;
using System.Diagnostics;
using System.IO.Ports;
using Microsoft.Practices.Prism.Logging;
using Service.IO.Buffers.Rx;
using Service.IO.Protocols.Rx;
using Service.IO.Protocols.Tx;
using Service.IO.TaskManager;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    public class ReadLockWorkerMaster : IOWorkerBase<byte>
    {
        #region ctors

        public ReadLockWorkerMaster(string port, SerialPortSettings serialPortSettings, ILoggerFacade logger) : 
            base(port, serialPortSettings, logger) { }

        #endregion

        #region methods

        public override byte Run(IInTaskManager taskManager)
        {
            var protocolTx = new ProtocolReadLockTx(serialPortSettings.HeaderTX);
            var protocolRx = new ProtocolReadLockRx(serialPortSettings.HeaderRX);
            var bufferRx = new IOBufferReadLockRx();

            using (var sp = new SerialPortManager(portName, (int)serialPortSettings.BaudRate,
                serialPortSettings.Parity, serialPortSettings.StopBits, 50, SerialPort.InfiniteTimeout))
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                for (;;)
                {
                    Transmit(sp, protocolTx, taskManager.SynchronizationContext);
                    RestartReceiv();
                    stopWatch.Restart();
                    while (stopWatch.Elapsed < TimeSpan.FromMilliseconds(100))
                    {
                        taskManager.IfCancellation();
                        if (IfReceived(sp, protocolRx, bufferRx, taskManager.SynchronizationContext))
                        {
                            stopWatch.Stop();
                            return protocolRx.Lock;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
