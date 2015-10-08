using Service.IO.Buffers;
using Service.IO.Buffers.Tx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Tx
{
    public class ProtocolConnectionTx : ProtocolBase
    {
        #region consts
 
        private const string request = "CONNECTION2560";

        #endregion

        #region ctors

        public ProtocolConnectionTx(byte header) : base(header, 0x55, Mode.Connection, new CheckSumXOR7FManager())
        {
        }

        #endregion

        #region properties

        public string Request { get { return request; } }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            IOBufferConnectionTx buffer = new IOBufferConnectionTx();
            
            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;
            for (int i = 0; i < Request.Length; i++)
                buffer[3 + i] = (byte)Request[i];

            checkSumManager.Calculate(buffer);
            
            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;

            for (int i = 0; i < Request.Length; i++)
                if (buffer[3 + i] != Request[i])
                    return false;

            return true;
        }

        #endregion
    }
}
