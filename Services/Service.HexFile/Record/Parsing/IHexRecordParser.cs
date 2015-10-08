namespace Service.HexFile.Record.Parsing
{
    /// <summary>
    /// Record parsing 
    /// </summary>
    internal interface IHexRecordParser
    {
        HexRecord Parse(string record);
        string UnParse(HexRecord hexRecord);
    }
}
