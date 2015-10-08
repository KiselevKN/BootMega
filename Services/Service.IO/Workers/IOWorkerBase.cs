using System;
using System.Threading;
using Microsoft.Practices.Prism.Logging;
using Service.IO.Buffers;
using Service.IO.Protocols;
using Service.IO.TaskManager;
using Service.Settings.Models;

namespace Service.IO.Workers
{
    /// <summary>
    /// Base class for an exchange with the microcontroller on the serial port
    /// </summary>
    public abstract class IOWorkerBase
    {
        #region fields

        /// <summary>
        /// Serial port name
        /// </summary>
        protected string portName;

        /// <summary>
        /// Serial port settings
        /// </summary>
        protected SerialPortSettings serialPortSettings;
        
        /// <summary>
        /// Logging
        /// </summary>
        protected ILoggerFacade logger;

        /// <summary>
        /// The counter of the received bytes
        /// </summary>
        protected int counterOfTheReceivedBytes;

        /// <summary>
        /// Step of receiving bytes
        /// </summary>
        protected byte step;

        #endregion

        #region ctors

        public IOWorkerBase(string portName, SerialPortSettings serialPortSettings, ILoggerFacade logger)
        {
            this.portName = portName;
            this.serialPortSettings = serialPortSettings;
            this.logger = logger;
        }

        #endregion

        #region methods

        protected void RestartReceiv()
        {
            counterOfTheReceivedBytes = 0;
            step = 0;
        }

        protected void Transmit(SerialPortManager sp, ProtocolBase protocol, SynchronizationContext syncContext)
        {
            var buffer = protocol.Pack();
            syncContext.Post(delegate { 
                logger.Log(String.Format("Tx: {0}", buffer.ToString()), Category.Debug, Priority.None); 
            }, null);
            sp.Write(protocol.Pack());
        }

        protected bool IfReceived(SerialPortManager sp, ProtocolBase protocol, IOBuffer buffer, SynchronizationContext syncContext)
        {
            byte receivedByte;
            if (sp.ReadByte(out receivedByte))
            {
                switch (step)
                {
                    case 0:
                        if (receivedByte == protocol.Header)
                        {
                            step++;
                            buffer[counterOfTheReceivedBytes++] = receivedByte;
                        }
                        break;
                    case 1:
                        if ((receivedByte & 0x80) == 0x80)
                            RestartReceiv();
                        else
                        {
                            buffer[counterOfTheReceivedBytes++] = receivedByte;
                            if (counterOfTheReceivedBytes == buffer.Size)
                            {
                                if (protocol.UnPack(buffer))
                                {
                                    syncContext.Post(delegate
                                    {
                                        logger.Log(String.Format("Rx: {0}", buffer.ToString()), Category.Debug, Priority.None);
                                    }, null);
                                    return true;
                                }
                                else
                                    RestartReceiv();
                            }
                        }
                        break;
                }
            }
            return false;
        }

        #endregion
    }

    public abstract class IOWorkerBase<TProgressValue, TResult> : IOWorkerBase
    {
        public IOWorkerBase(string portName, SerialPortSettings serialPortSettings, ILoggerFacade logger)
            : base(portName, serialPortSettings, logger) { }

        public abstract TResult Run(IInTaskManager<TProgressValue> taskManager);
    }

    public abstract class IOWorkerBase<TResult> : IOWorkerBase
    {
        public IOWorkerBase(string portName, SerialPortSettings serialPortSettings, ILoggerFacade logger)
            : base(portName, serialPortSettings, logger) { }

        public abstract TResult Run(IInTaskManager taskManager);
    }
}
