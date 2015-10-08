namespace Service.HexFile.Record.Parsing
{
    using System;

    /// <summary>
    /// Record parsing 
    /// </summary>
    internal interface IHexRecordParser
    {
        HexRecord Parse(String record);
        String UnParse(HexRecord hexRecord);
    }
}
