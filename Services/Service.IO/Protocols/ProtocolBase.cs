using Service.IO.Buffers;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols
{
    /// <summary>
    /// Base class Tx/Rx protocols
    /// </summary>
    public abstract class ProtocolBase
    {
        #region fields

        private Mode regim;
        private byte header;
        private byte confirmation;
        protected ICheckSumManager checkSumManager;

        #endregion

        #region ctors

        public ProtocolBase(byte header, byte confirmation, Mode regim, ICheckSumManager checkSumManager)
        {
            this.header = header;
            this.confirmation = confirmation;
            this.regim = regim;
            this.checkSumManager = checkSumManager;
        }

        #endregion

        #region properties

        public byte Header { get { return header; } }
        public byte Confirmation { get { return confirmation; } }
        public Mode Regim { get { return regim; } }

        #endregion

        #region methods

        public abstract IOBuffer Pack();

        public virtual bool UnPack(IOBuffer buffer)
        {
            if (!checkSumManager.IsValid(buffer))
                return false;
            if ((buffer[0] != Header) || (buffer[1] != Confirmation) || (buffer[2] != (byte)Regim))
                return false;

            return true;
        }

        #endregion
    }
}
