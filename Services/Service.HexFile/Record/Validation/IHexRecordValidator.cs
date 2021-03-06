﻿namespace Service.HexFile.Record.Validation
{
    /// <summary>
    /// Record validation 
    /// </summary>
    internal interface IHexRecordValidator
    {
        /// <summary>Validate</summary>
        /// <param name="record">Hex file record</param>
        /// <exception cref="HexRecordValidationException">Throw if record is invalid</exception>
        void Validate(string record);
    }
}
