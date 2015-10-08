using Service.IO.Buffers;
using Service.IO.Buffers.Tx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Tx
{
    public class ProtocolReadFuseTx : ProtocolBase
    {
        #region ctors

        public ProtocolReadFuseTx(byte header)
            : base(header, 0x55, Mode.ReadFuse, new CheckSumXOR7FManager()) { }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferReadFuseTx();

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
