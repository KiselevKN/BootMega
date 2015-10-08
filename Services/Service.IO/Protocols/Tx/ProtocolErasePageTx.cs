using Service.IO.Buffers;
using Service.IO.Buffers.Tx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Tx
{
    public class ProtocolErasePageTx : ProtocolBase
    {
        #region ctors

        public ProtocolErasePageTx(byte header, Mode regim)
            : base(header, 0x55, regim, new CheckSumXOR7FManager()) { }

        #endregion

        #region properties

        public int PageNumber { get; set; }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferErasePageTx();
            
            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;
            buffer[3] = (byte)(PageNumber & 0x7F);
            buffer[4] = (byte)((PageNumber >> 7) & 0x7F);
            buffer[5] = (byte)((PageNumber >> 14) & 0x7F);
            buffer[6] = (byte)((PageNumber >> 21) & 0x07);

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;

            PageNumber = (int)(buffer[6] << 21) +
                (int)(buffer[5] << 14) + (int)(buffer[4] << 7) + buffer[3];

            return true;
        }

        #endregion
    }
}
