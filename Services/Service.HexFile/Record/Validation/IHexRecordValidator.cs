namespace Service.HexFile.Record.Validation
{   
    using System;

    /// <summary>
    /// Record validation 
    /// </summary>
    internal interface IHexRecordValidator
    {
        /// <summary>Validate</summary>
        /// <param name="record">Hex file record</param>
        /// <exception cref="HexRecordValidationException">Throw if record is invalid</exception>
        void Validate(String record);
    }
}
