using System;
using Service.IO.Buffers;
using Service.IO.Buffers.Rx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Rx
{
    public class ProtocolIsEmptyPageRx : ProtocolBase
    {
        #region ctors

        public ProtocolIsEmptyPageRx(byte header, Mode regim) : base(header, 0x5A, regim, new CheckSumXOR7FManager())
        {
        }

        #endregion

        #region properties

        public int PageNumber { get; set; }
        public bool IsEmpty { get; set; }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferIsEmptyPageRx();

            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;
            buffer[3] = (byte)(PageNumber & 0x7F);
            buffer[4] = (byte)((PageNumber >> 7) & 0x7F);
            buffer[5] = (byte)((PageNumber >> 14) & 0x7F);
            buffer[6] = (byte)((PageNumber >> 21) & 0x07);
            buffer[7] = Convert.ToByte(IsEmpty);

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;
            PageNumber = (buffer[6] << 21) + (buffer[5] << 14) + (buffer[4] << 7) + buffer[3];
            IsEmpty = (buffer[7] == 1);

            return true;
        }

        #endregion
    }

}
