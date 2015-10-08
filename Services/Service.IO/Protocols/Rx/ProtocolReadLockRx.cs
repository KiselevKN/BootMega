using Service.IO.Buffers;
using Service.IO.Buffers.Rx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Rx
{
    public class ProtocolReadLockRx : ProtocolBase
    {
        #region ctors

        public ProtocolReadLockRx(byte header) : base(header, 0x5A, Mode.ReadLock, new CheckSumXOR7FManager())
        {
        }

        #endregion

        #region properties

        public byte Lock { get; set; }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferReadLockRx();

            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;
            buffer[3] = (byte)(Lock & 0x7F);
            buffer[4] = (byte)((Lock >> 7) & 0x01);

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;

            Lock = (byte)((buffer[4] << 7) + buffer[3]);

            return true;
        }

        #endregion
    }

}
