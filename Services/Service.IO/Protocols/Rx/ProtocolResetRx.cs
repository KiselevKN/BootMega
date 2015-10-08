using Service.IO.Buffers;
using Service.IO.Buffers.Rx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Rx
{
    public class ProtocolResetRx : ProtocolBase
    {
        #region consts

        private const byte extendedConfirmation = 1;

        #endregion

        #region ctors

        public ProtocolResetRx(byte header) : base(header, 0x5A, Mode.Reset, new CheckSumXOR7FManager())
        {
        }

        #endregion

        #region properties

        public byte ExtendedConfirmation
        {
            get
            {
                return extendedConfirmation;
            }
        }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferResetRx();

            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;
            buffer[3] = ExtendedConfirmation;

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;
                
            if (buffer[3] != ExtendedConfirmation)
                return false;

            return true;
        }

        #endregion
    }
}
