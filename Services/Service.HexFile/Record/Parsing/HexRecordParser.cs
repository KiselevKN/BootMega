namespace Service.HexFile.Record.Parsing
{
    internal class HexRecordParser : IHexRecordParser
    {
        #region IHexRecordParser Members

        public HexRecord Parse(string record)
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

        public string UnParse(HexRecord hexRecord)
        {
            return hexRecord.ToString();
        }

        #endregion
    }
}
