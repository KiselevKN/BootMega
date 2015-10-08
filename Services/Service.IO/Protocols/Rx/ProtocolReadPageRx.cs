using Service.HexFile.MemoryMapping;
using Service.IO.Buffers;
using Service.IO.Buffers.Rx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Rx
{
    public class ProtocolReadPageRx : ProtocolBase
    {
        #region ctors

        public ProtocolReadPageRx(byte header, Mode regim) : base(header, 0x5A, regim, new CheckSumXOR7FManager())
        {
            Line = new PageLine();
        }

        #endregion

        #region properties

        public int PageNumber { get; set; }
        public int PageLineNumber { get; set; }
        public PageLine Line { get; set; }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferReadPageRx();

            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;

            buffer[3] = (byte)(PageNumber & 0x7F);
            buffer[4] = (byte)((PageNumber >> 7) & 0x7F);
            buffer[5] = (byte)((PageNumber >> 14) & 0x7F);
            buffer[6] = (byte)((PageNumber >> 21) & 0x07);

            buffer[7] = (byte)(PageLineNumber & 0x7F);

            for (int i = 0; i < PageLine.Size; i++)
                buffer[8 + i] = (byte)(Line[i] & 0x7F);

            for (int k = 0; k < 7; k++)
            {
                buffer[24] |= (byte)((Line[k] >> (7 - k)) & (0x01 << k));
                buffer[25] |= (byte)((Line[7 + k] >> (7 - k)) & (0x01 << k));
            }
            buffer[24] &= 0x7F;
            buffer[25] &= 0x7F;

            buffer[26] |= (byte)((Line[14] >> 7) & 0x01);
            buffer[26] |= (byte)((Line[15] >> 6) & 0x02);
            buffer[26] &= 0x03;

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;

            PageNumber = (buffer[6] << 21) +
                (buffer[5] << 14) + (buffer[4] << 7) + buffer[3];
            PageLineNumber = buffer[7];

            for (int i = 0; i < PageLine.Size; i++)
                Line[i] = buffer[8 + i];

            for (int m = 0; m < 7; m++)
            {
                Line[m] |= (byte)((buffer[24] << (7 - m)) & 0x80);
                Line[7 + m] |= (byte)((buffer[25] << (7 - m)) & 0x80);
            }

            Line[14] |= (byte)((buffer[26] << 7) & 0x80);
            Line[15] |= (byte)((buffer[26] << 6) & 0x80);

            return true;
        }

        #endregion
    }
}
