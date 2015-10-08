namespace Service.HexFile.Record.Parsing
{
    using System;

    internal class HexRecordParser : IHexRecordParser
    {
        #region IHexRecordParser Members

        public HexRecord Parse(String record)
        {
            switch(record.GetRecordType())
            {
                case HexRecordType.ExtendedLinearAddress:
                case HexRecordType.ExtendedSegmentAddress:
                    return new AddressHexRecord(record);
                default:
                    return new DataHexRecord(record);
            }
        }

        public String UnParse(HexRecord hexRecord)
        {
            return hexRecord.ToString();
        }

        #endregion
    }
}
