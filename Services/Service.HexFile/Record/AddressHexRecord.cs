using System.Collections.Generic;

namespace Service.HexFile.Record
{
    /// <summary>
    /// Hex record with address 
    /// </summary>
    internal class AddressHexRecord : HexRecord
    {
        #region private fields

        private HexRecordType _type;
        private long _address;

        #endregion

        #region ctors

        internal AddressHexRecord(HexRecordType type, long address)
        {
            _type = type;
            _address = address;
        }

        internal AddressHexRecord(string record)
        {
            _type = record.GetRecordType();
            _address = record.GetAddress();
        }

        #endregion

        #region HexRecord overrides

        internal override HexRecordType RecordType
        {
            get { return _type; }
        }

        internal override long Address
        {
            get { return _address; }
        }

        internal override IEnumerable<byte> AsBytes()
        {
            List<byte> list = new List<byte>();
            list.Add(0x02);
            list.Add(0x00);
            list.Add(0x00);
            list.Add((byte)RecordType);
            list.Add((RecordType == HexRecordType.ExtendedLinearAddress) ? (byte)(Address >> 24) : (byte)(Address >> 12));
            list.Add((RecordType == HexRecordType.ExtendedLinearAddress) ? (byte)(Address >> 16) : (byte)(Address >> 4));
            list.Add(list.Checksum());
            return list;
        }

        #endregion
    }
}
