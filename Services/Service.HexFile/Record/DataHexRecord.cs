namespace Service.HexFile.Record
{
    using System.Collections.Generic;

    /// <summary>
    /// Hex record with data
    /// </summary>
    internal class DataHexRecord : HexRecord
    {
        #region private fields

        private HexRecordType _type;
        private long _address;
        private byte[] _data;

        #endregion

        #region ctors

        internal DataHexRecord(HexRecordType type, long address, byte[] data)
        {
            _type = type;
            _address = address;
            _data = data;
        }

        internal DataHexRecord(string record)
        {
            _type = record.GetRecordType();
            _address = record.GetAddress();
            _data = record.GetDataBytes();
        }

        #endregion

        #region properties

        internal byte[] Data
        {
            get { return _data; }
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
            list.Add((byte)Data.Length);
            list.Add((byte)(Address >> 8));
            list.Add((byte)(Address));
            list.Add((byte)RecordType);
            foreach (var b in Data)
                list.Add(b);
            list.Add(list.Checksum());

            return list;
        }

        #endregion
    }
}
