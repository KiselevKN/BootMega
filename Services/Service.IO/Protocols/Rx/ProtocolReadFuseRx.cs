using Service.IO.Buffers;
using Service.IO.Buffers.Rx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Rx
{
    public class ProtocolReadFuseRx : ProtocolBase
    {
        #region ctors

        public ProtocolReadFuseRx(byte header)
            : base(header, 0x5A, Mode.ReadFuse, new CheckSumXOR7FManager()) { }

        #endregion

        #region properties

        public byte ExtendedFuses { get; set; }
        public byte HightFuses { get; set; }
        public byte LowFuses { get; set; }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferReadFuseRx();

            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;

            buffer[3] = (byte)(ExtendedFuses & 0x7F);
            buffer[4] = (byte)(HightFuses & 0x7F);
            buffer[5] = (byte)(LowFuses & 0x7F);
            buffer[6] = (byte)((ExtendedFuses >> 7) & 0x01);               
            buffer[6] |= (byte)((HightFuses >> 6) & 0x02);
            buffer[6] |= (byte)((LowFuses >> 5) & 0x04);

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;

            ExtendedFuses = (byte)(buffer[3] + ((buffer[6] << 7) & 0x80));
            HightFuses = (byte)(buffer[4] + ((buffer[6] << 6) & 0x80));
            LowFuses = (byte)(buffer[5] + ((buffer[6] << 5) & 0x80));

            return true;
        }

        #endregion
    }
}
