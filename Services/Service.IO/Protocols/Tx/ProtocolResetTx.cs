using Service.IO.Buffers;
using Service.IO.Buffers.Tx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Tx
{
    public class ProtocolResetTx : ProtocolBase
    {
        #region ctors

        public ProtocolResetTx(byte header)
            : base(header, 0x55, Mode.Reset, new CheckSumXOR7FManager()) { }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferResetTx();
            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;

            return true;
        }

        #endregion
    }
}
