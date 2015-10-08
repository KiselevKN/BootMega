using System;
using Service.IO.Buffers;
using Service.IO.Buffers.Rx;
using Service.IO.CheckSumManager;

namespace Service.IO.Protocols.Rx
{
    public class ProtocolConnectionRx : ProtocolBase
    {
        #region consts

        private const string confirmationString = "BTLoader2560";

        #endregion

        #region ctors

        public ProtocolConnectionRx(byte header)
            : base(header, 0x5A, Mode.Connection, new CheckSumXOR7FManager()) { }

        #endregion

        #region properties

        /// <summary>
        /// Confirmation string
        /// </summary>
        public string ConfirmationString { get { return confirmationString; } }
        
        /// <summary>
        /// Firmware version
        /// </summary>
        public int Version { get; set; }
        
        /// <summary>
        /// Сreation date firmware
        /// </summary>
        public DateTime Date { get; set; }

        #endregion

        #region methods

        public override IOBuffer Pack()
        {
            var buffer = new IOBufferConnectionRx();

            buffer[0] = Header;
            buffer[1] = Confirmation;
            buffer[2] = (byte)Regim;

            for (int i = 0; i < ConfirmationString.Length; i++)
                buffer[3 + i] = (byte)ConfirmationString[i];

            string ver = Version.ToString("00");
            buffer[15] = (byte)ver[0];
            buffer[16] = (byte)ver[1];

            string day = Date.Day.ToString("00");
            buffer[17] = (byte)day[0];
            buffer[18] = (byte)day[1];

            string month = Date.Month.ToString("00");
            buffer[19] = (byte)month[0];
            buffer[20] = (byte)month[1];

            string year = Date.Year.ToString("0000");
            buffer[21] = (byte)year[2];
            buffer[22] = (byte)year[3];

            checkSumManager.Calculate(buffer);

            return buffer;
        }

        public override bool UnPack(IOBuffer buffer)
        {
            if (!base.UnPack(buffer))
                return false;

            for (int i = 0; i < ConfirmationString.Length; i++)
                if (buffer[3 + i] != (byte)ConfirmationString[i])
                    return false;

            Version = Convert.ToInt32(new string(new char[] { (char)buffer[15], (char)buffer[16] }));
            int day = Convert.ToInt32(new string(new char[] { (char)buffer[17], (char)buffer[18] }));
            int month = Convert.ToInt32(new string(new char[] { (char)buffer[19], (char)buffer[20] }));
            int year = 2000 + Convert.ToInt32(new string(new char[] { (char)buffer[21], (char)buffer[22] }));
            Date = new DateTime(year, month, day);

            return true;
        }

        #endregion
    }
}
