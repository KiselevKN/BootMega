namespace Service.HexFile.Record.Validation
{
    using System;
    using Service.HexFile.Properties;

    /// <summary>
    /// Record length validator
    /// </summary>
    internal class HexRecordLengthValidator : IHexRecordValidator
    {
        #region ctors

        internal HexRecordLengthValidator() { }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(String record)
        {
            if (record.GetNumberOfDataBytes() != record.GetNumberOfDataBytesInFact())
                throw new HexRecordValidationException(String.Format(Resources.InvalidRecordLength, record));
        }

        #endregion
    }
}
